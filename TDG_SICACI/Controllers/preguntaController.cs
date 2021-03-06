﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;
using JertiFramework.Interpretes;
using JertiFramework.Controls;
using TDG_SICACI.Database.DAL;
using System.Text;
using System.Net;


namespace TDG_SICACI.Controllers
{
    public class PreguntaController : BaseController
    {

        private const string kUserRol = "Administrador,RD";
        //
        // GET: /pregunta/

        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public JsonResult _get_grid_preguntas(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IPreguntas.GetPreguntaList().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IPreguntas.GetPreguntaList());

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_PreguntasViewModel
                {
                    ID_Jerarquia = u.ID_PREGUNTA.Value,
                    Descripcion_Jerarquia = u.DESCRIPCION_JERARQUIA,
                    Tipo_Pregunta = u.TIPO_PREGUNTA,
                    Asociado_A = u.ASOCIADO_A,
                    Arbol = u.ARBOL,
                    GIDEM = u.CLASIFICACION
                });

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = db.IPreguntas.GetPreguntaList().Count(),
                page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public ActionResult AgregarPregunta()
        {
            return View();
        }

        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public ActionResult _nueva_pregunta(string padd, string tipo)
        {
            ViewBag.TipoPregunta = tipo;
            if (padd.Equals("S")) return PartialView("_nueva_pregunta_gidem");

            return PartialView();
        }

        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public ActionResult _norma_iso()
        {
            SICACI_DAL db = new SICACI_DAL();

            return PartialView(db.IPreguntas.GetNormaISO());
        }

        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public ActionResult _multiple_preguntas()
        {
            return PartialView();
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public JsonResult _new_pregunta_abierta(Models.newPreguntaModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se puede continuar debido a que el formulario se encuentra incompleto. Por favor, corrija los errores y vuelva a intentarlo",
                                                        titulo: "Formulario Incompleto",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            var db = new SICACI_DAL();
            var item = db.IPreguntas.GetPreguntaList()
                .Where(p => p.ID_JERARQUIA.Equals(model.ReferenciaA) && p.CLASIFICACION.Equals("S")).FirstOrDefault();
            db.IPreguntas.NewPregunta_Abierta(
                model.FormPregunta.TextoPregunta,
                model.FormPregunta.ComentarioPregunta,
                model.FormPregunta.TipoDocumento,
                (model.PreguntaGIDEM == "S" ? 0 : 1),
                model.ReferenciaA,
                model.OrdenVisual,
                User.Identity.Name,
                model.FormPregunta.LinkComentario);

            if (model.PreguntaGIDEM.Equals("S"))
            {
                //Pregunta adicional
                db.IUsers.RegistrarEventoBitacora("Jerarquía de Preguntas", User.Identity.Name, "Se ha creado una nueva pregunta adicional", string.Empty,
                    model.FormPregunta.TextoPregunta);
            }
            else
            {
                //Pregunta de la Norma ISO 9001
                db.IUsers.RegistrarEventoBitacora("Jerarquía de Preguntas", User.Identity.Name, "Se ha sustituido una pregunta de la Norma ISO 9001 por una nueva.", item.DESCRIPCION_JERARQUIA,
                    model.FormPregunta.TextoPregunta);
            }

            return Json(new
            {
                success = true,
                redirectURL = Url.Action("Index", "Pregunta")
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public JsonResult _new_pregunta_multiple(Models.newPreguntaMultipleModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se puede continuar debido a que el formulario se encuentra incompleto. Por favor, corrija los errores y vuelva a intentarlo",
                                                        titulo: "Formulario Incompleto",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            /*Verificamos el tipo de pregunta para determinar cuantas respuestas correctas puede contener*/
            if (model.TipoPregunta.Equals("OM"))
            {   //Opción Multiple 
                if (model.Respuestas.Where(r => r.EsCorrecta.Equals("S")).Count() != 1)
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new
                    {
                        notify = new JFNotifySystemMessage("Las preguntas de opción multiple requieren que una y solo una de las respuesta sea marcada como correcta. Por favor, corrija los errores y vuelva a intentarlo",
                                                            titulo: "Multiples respuestas",
                                                            permanente: false,
                                                            tiempo: 5000)
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {   //Selección Multiple
                if (model.Respuestas.Where(r => r.EsCorrecta.Equals("S")).Count() == 0)
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new
                    {
                        notify = new JFNotifySystemMessage("Las preguntas de selección multiple requieren que al menos una de las respuesta sea marcada como correcta. Por favor, corrija los errores y vuelva a intentarlo",
                                                            titulo: "Multiples respuestas",
                                                            permanente: false,
                                                            tiempo: 5000)
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            var db = new SICACI_DAL();
            var item = db.IPreguntas.GetPreguntaList()
                .Where(p => p.ID_JERARQUIA.Equals(model.ReferenciaA) && p.CLASIFICACION.Equals("S")).FirstOrDefault();
            db.IPreguntas.NewPregunta_Multiple(
                model.FormPregunta.TextoPregunta,
                model.FormPregunta.ComentarioPregunta,
                model.FormPregunta.TipoDocumento,
                (model.PreguntaGIDEM == "S" ? 0 : 1),
                model.ReferenciaA,
                model.OrdenVisual,
                User.Identity.Name,
                model.TipoPregunta,
                model.Respuestas,
                model.FormPregunta.LinkComentario);

            if (model.PreguntaGIDEM.Equals("S"))
            {
                //Pregunta adicional
                db.IUsers.RegistrarEventoBitacora("Jerarquía de Preguntas", User.Identity.Name, "Se ha creado una nueva pregunta adicional", string.Empty,
                    model.FormPregunta.TextoPregunta);
            }
            else
            {
                //Pregunta de la Norma ISO 9001
                db.IUsers.RegistrarEventoBitacora("Jerarquía de Preguntas", User.Identity.Name, "Se ha sustituido una pregunta de la Norma ISO 9001 por una nueva.", item.DESCRIPCION_JERARQUIA,
                    model.FormPregunta.TextoPregunta);
            }

            return Json(new
            {
                success = true,
                redirectURL = Url.Action("Index", "Pregunta")
            });
        }
        /* de aca hacia abajo es modificacion Nelson para deshabilitar pregunta añadir para hacer cambio*/
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public JsonResult Eliminar(int Arbol)
        {
            //Console.WriteLine(Arbol);
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            //if (string.IsNullOrWhiteSpace(ID_Jerarquia))
            //{
            //    Response.TrySkipIisCustomErrors = true;
            //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return Json(new
            //    {
            //        notify = new JFNotifySystemMessage("No se ha podido eliminar la pregunta debido a que no existe o no se ha especificado ninguna pregunta",
            //                                            titulo: "Eliminación de Pregunta",
            //                                            permanente: false,
            //                                            tiempo: 5000)
            //    }, JsonRequestBehavior.AllowGet);
            //}

            SICACI_DAL db = new SICACI_DAL();
            var dataPregunta = db.IPreguntas.GetPregunta(Arbol);
            db.IPreguntas.EliminarPreguntaGIDEM(Arbol);
            db.IUsers.RegistrarEventoBitacora("Jerarquía de Preguntas", User.Identity.Name,
                   "Se ha eliminado una pregunta adicional", string.Empty,
                   string.Format("ID: {0}; Pregunta: {1}; Tipo de Pregunta: {2}", Arbol, dataPregunta.TEXTO_PREGUNTA, dataPregunta.TIPO_PREGUNTA));

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("La pregunta se ha eliminado correctamente.", titulo: "Eliminación de Pregunta", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Modificar(Models.PreguntaModifiyModel model, int ID)
        {
            //Validamos que se nos haya transferido el usuario a ser modificado
            //if (string.IsNullOrWhiteSpace(OrdenVisual))
            //{
            //    Response.TrySkipIisCustomErrors = true;
            //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return Json(new
            //    {
            //        notify = new JFNotifySystemMessage("No se ha especificado el usuario al cual se desea realizar dicha modificación",
            //                                            titulo: "Modificación de Datos de Usuario",
            //                                            permanente: false,
            //                                            tiempo: 5000)
            //    }, JsonRequestBehavior.AllowGet);
            //}

            SICACI_DAL db = new SICACI_DAL();
            db.IPreguntas.ModificarPreguntaGIDEM(ID, model.OrdenVisual);
            db.IUsers.RegistrarEventoBitacora("Jerarquía de Preguntas", User.Identity.Name, "Se ha modificado el orden visual de una pregunta adicional.",
                string.Format("ID de pregunta modificada: {0}", ID), model.OrdenVisual.ToString());
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El usuario se ha modificado correctamente.", titulo: "Completado", permanente: true, icono: JFNotifySystemIcon.Update)
            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public ActionResult _get_modificar_pregunta(int ID)
        {
        //Debemos validar que se haya pasado un usuario en la solicitud
        //    if (string.IsNullOrWhiteSpace(Orden))
        //{
        //    Response.TrySkipIisCustomErrors = true;
        //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //    return Json(new
        //    {
        //        notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el usuario que se desea modificar.",
        //                                            titulo: "Modificación de Usuario",
        //                                            permanente: false,
        //                                            tiempo: 5000)
        //    }, JsonRequestBehavior.AllowGet);
        //}

       // Si esta correcto, recuperamos la información de la pregunta especificada
        SICACI_DAL db = new SICACI_DAL();
        var dataPregunta = db.IPreguntas.GetPregunta(ID);
        return PartialView(new Models.PreguntaModifiyModel {
                //OrdenVisual = dataPregunta.DESCRIPCION_JERARQUIA,
                //TextoPregunta = dataPregunta.TEXTO_PREGUNTA
 
        });

    }
    }
}
