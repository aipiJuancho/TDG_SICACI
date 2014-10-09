using System;
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
                    idUsuario = u.idUsuario
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Consultar(string revision)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (string.IsNullOrWhiteSpace(revision))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud la evaluacion que se desea consultar.",
                                                        titulo: "Consultar un Archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return View(new Models.Consultar_EvaluacionModel
            {
                 revision = 8, 
                 fechaCreacion = DateTime.Now, 
                 comentario="comentario de la evaluacion", 
                 idUsuario= "sofy"
            });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar(string revision)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (string.IsNullOrWhiteSpace(revision))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud la evaluacion que se desea modificar.",
                                                        titulo: "Modificación de Usuario",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }
            return PartialView(new Models.Modificar_EvaluacionModel
            {
                comentario = "comentario a actualizar"
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
