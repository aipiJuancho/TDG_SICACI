﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;
using JertiFramework.Interpretes;
using TDG_SICACI.Database.DAL;
using System.Net;
using JertiFramework.Controls;
using System.Data;
using System.IO;
using System.Globalization;

namespace TDG_SICACI.Controllers
{
    public class EvaluacionController : BaseController
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol = "Administrador";
        private const string kItemType = "Evaluacion";
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        public ActionResult Index()
        {
            if (User.IsInRole(kUserRol))
            {
                return View();
            }
            else if ((User.IsInRole("Consultor Externo")) || (User.IsInRole("Consultor Interno")))
            {
                return RedirectToAction("Agregar");
            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        [Authorize(Roles = kUserRol)]
        public JsonResult DataGrid(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IPreguntas.GetEvaluaciones().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IPreguntas.GetEvaluaciones());

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_EvaluacionViewModel()
                {
                    revision = u.revision,
                    fechaCreacion = u.fechaCreacion.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")),
                    comentario = u.comentario,
                    idUsuario = u.idUsuario,
                    estado = u.estado,
                    fechaRevision = (u.fechaRevision.HasValue ? u.fechaRevision.Value.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")) : string.Empty)
                });
            
            return Json(new jfBSGrid_ReturnData
            {
                total_rows = db.IPreguntas.GetEvaluaciones().Count(),
                page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Create
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador,Consultor Externo,Consultor Interno")]
        public ActionResult Agregar()
        {
            SICACI_DAL db = new SICACI_DAL();
            ViewBag.Headers = db.IPreguntas.GetNormaISO().Where(n => n.NIVEL.Equals(0)).AsEnumerable();
            ViewBag.Resto = db.IPreguntas.GetNormaISO().Where(n => !n.NIVEL.Equals(0)).AsEnumerable();
            ViewBag.Self = db.IPreguntas.GetInfoSelf();
            return View();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = "Administrador,Consultor Externo,Consultor Interno")]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_EvaluacionModel model)//TODO: comprobar el Modelo
        {
            SICACI_DAL db = new SICACI_DAL();
            // db.IUsers.CrearUsuario(model.Usuario, model.Nombres, model.Apellidos, model.CorreoE, model.Password, model.Rol);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("La Evaluacion se ha creado correctamente", titulo: "Nuevo " + kItemType, permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _save_evaluacion(Models.Responses_SelfAssessment model)
        {
            //Hacemos unas validaciones en los datos recibidos
            if (model.TipoPregunta.Count().Equals(0))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha enviado ninguna respuesta del Self-Assessment",titulo: "Self-Assessment en Blanco",permanente: false,tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            /*DEFINIMOS Y CONSTRUIMOS LA TABLA PARA PODER ENVIAR LAS RESPUESTAS*/
            DataTable udt_Evaluacion = new DataTable();
            udt_Evaluacion.Columns.Add("Id_Preg", typeof(int));
            udt_Evaluacion.Columns.Add("ID_Opcion", typeof(int));
            udt_Evaluacion.Columns.Add("Respuesta", typeof(string));
            udt_Evaluacion.Columns.Add("Comentario", typeof(string));

            for (int i = 0; i < model.TipoPregunta.Count(); i++)
            {
                udt_Evaluacion.Rows.Add(model.ID_Pregunta.ElementAt(i),
                    model.ID_Respuesta.ElementAt(i),
                    model.Respuesta.ElementAt(i),
                    "");
            }

            /*Guardamos todas las respuestas en la base de datos para poder generar el ID_SOLUCION*/
            SICACI_DAL db = new SICACI_DAL();
            int ID_Solucion = db.IPreguntas.SaveEvaluacion(User.Identity.Name, udt_Evaluacion);

            /*Ahora pasamos a verificar si existen archivos que debemos de guardar en el servidor*/
            if (model.Archivo != null)
            {
                if (model.Archivo.Count() > 0)
                {
                    HttpPostedFileBase file;
                    string fileName;
                    for (int i = 0; i < model.Archivo.Count(); i++)
                    {
                        file = model.Archivo.ElementAt(i);
                        if (file.ContentLength > 0) //Validamos que se haya cargado un archivo
                        {
                            if (file.ContentType.Equals("application/pdf")) //Validamos archivos PDF
                            {
                                fileName = string.Format("PDF_{1}_{0}.pdf", model.InfoArchivo.ElementAt(i), ID_Solucion);
                                var path = Path.Combine(Server.MapPath("~/App_Data/soluciones"), fileName);
                                file.SaveAs(path);
                                db.IPreguntas.AsociarDocumento_Respuesta(ID_Solucion, model.InfoArchivo.ElementAt(i), fileName);
                            }
                            else if (file.ContentType.Contains("image/"))
                            {
                                fileName = string.Format("IMG_{1}_{0}.{2}", model.InfoArchivo.ElementAt(i), ID_Solucion, file.FileName.Substring(file.FileName.Length - 3));
                                var path = Path.Combine(Server.MapPath("~/App_Data/soluciones"), fileName);
                                file.SaveAs(path);
                                db.IPreguntas.AsociarDocumento_Respuesta(ID_Solucion, model.InfoArchivo.ElementAt(i), fileName);
                            }
                        }
                    }
                }
            }

            return Json(new
            {
                success = true,
                redirectURL = Url.Action("Index", "Home")
            });
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Read

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador,Consultor Externo,Consultor Interno")]
        public ActionResult Consultar(int revision = 0)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (revision.Equals(0))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = "Lo sentimos, pero el ID de la revisón no es válido.";
                return View("Error");
            }
            var db = new SICACI_DAL();
            var info = db.IPreguntas.Revision_Info(revision);
            var jerarquias = db.IPreguntas.Revision_GruposJerarquia(revision);
            var incisos = new List<Models.inciso>();


            foreach (var grupo in jerarquias)
            {
                var preguntas = db.IPreguntas.Revision_PreguntasYRespuestas(revision, grupo.ID_JERARQUIA)
                    .Where(p => string.IsNullOrEmpty(p.TIPO_PREGUNTA).Equals(false))
                    .OrderBy(p => p.ORDEN_JERARQUIA).ToArray();

                var inciso = new Models.inciso();
                inciso.titulo = grupo.DESCRIPCION_JERARQUIA;
                inciso.preguntas = preguntas.Select(p => new Models.pregunta(){
                    id = p.ID_PREGUNTA.Value.ToString(),
                    interrogante = string.Format("{0} - {1}", p.ORDEN_JERARQUIA, p.PREGUNTA),
                    respuesta = p.RESPUESTA,
                    resultado = (p.RESULTADO.Equals(" ") ? "": p.RESULTADO),
                    tipo_pregunta = p.TIPO_PREGUNTA,
                    norma_gidem = p.NORMA_GIDEM
                }).ToList();

                incisos.Add(inciso);
            }

            return View(new Models.Consultar_EvaluacionModel
            {
                revision = info.ID_SOLUCION,
                fechaCreacion = info.FECHA_SOLUCION.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")),
                comentario = info.ULTIMO_COMENTARIO,
                idUsuario = info.NOMBRE_USUARIO,
                incisos = incisos
            });
        }


        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = kUserRol)]
        public ActionResult AgregarFinding()
        {
            //Definimos el ComboBox de Tipo de No Conformidad
            var NoConformidad = new List<SelectListItem>();
            NoConformidad.Add(new SelectListItem() { Text = "No conformidad mayor", Value = "1", Selected = true });
            NoConformidad.Add(new SelectListItem() { Text = "No conformidad menor", Value = "2" });
            NoConformidad.Add(new SelectListItem() { Text = "Observacion", Value = "3" });
            NoConformidad.Add(new SelectListItem() { Text = "Oportunidad de mejora", Value = "4" });

            //Definimos el ComboBox de Tipo de Correción
            var correccion = new List<SelectListItem>();
            correccion.Add(new SelectListItem() { Text = "Inmediata", Value = "IN", Selected = true });
            correccion.Add(new SelectListItem() { Text = "Sostenible", Value = "SO" });

            ViewBag.TipoNoConformidad = NoConformidad;
            ViewBag.TIpoCorreccion = correccion;
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult AgregarFinding(Models.Agregar_FindingModel model)//TODO: comprobar el Modelo
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IFindings.Create_Finding(model.tipoNoConformidad, model.comentario, model.tipoCorreccion,
                model.accionCorrectivaSugerida, model.fechaLimiteSugerida, User.Identity.Name);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El " + kItemType + " se ha creado correctamente", titulo: "Nuevo " + kItemType, permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }



        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar(int revision = 0)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (revision.Equals(0))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud la evaluación que se desea modificar.",
                                                        titulo: "Añadir comentario a Evaluación",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            var db = new SICACI_DAL();
            var item = db.IPreguntas.GetComentarios().Where(c => c.ID_SOLUCION.Equals(revision)).FirstOrDefault();
            return PartialView(new Models.Modificar_EvaluacionModel
            {
                comentario = (item == null ? string.Empty : item.COMENTARIO)
            });
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Modificar(Models.Modificar_EvaluacionModel model, int revision)
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IPreguntas.SaveComentario(model.comentario, revision, User.Identity.Name);

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Se añadio el comentario correctamente a la evaluación", titulo: "Modificación del " + kItemType, permanente: true, icono: JFNotifySystemIcon.Update)
            });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public JsonResult Eliminar(string revision)
        {
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            if (string.IsNullOrWhiteSpace(revision))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido eliminar la evaluacion debido a que no existe o no se ha especificado ningun archivo",
                                                        titulo: "Eliminación de archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("La evaluacion se ha eliminado correctamente.", titulo: "Eliminación de Archivo", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion

    }
}
