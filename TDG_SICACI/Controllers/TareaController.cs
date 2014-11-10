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

namespace TDG_SICACI.Controllers
{
    public class TareaController : BaseController
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol = "Administrador";
        private const string kItemType = "Tarea";
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
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        [Authorize(Roles = kUserRol)]
        public JsonResult DataGrid(jfBSGrid_Respond model)
        {
            //la data es dummy, por eso no funciona la paginacion correctamente porque como este metodo se ejecuta con cada 
            //request ajax la data se presenta siempre estatica

            List<Models.Grid_TareaViewModel> items = new List<Models.Grid_TareaViewModel>()
            {
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14},
                new Models.Grid_TareaViewModel { orden = 1, titulo = "Titulo de la tarea", responable = "Juan", fechaFinalizacion = DateTime.Today, progreso = 14}
            };
            return Json(new jfBSGrid_ReturnData
            {
                total_rows = items.Count(),
                page_data = items
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Create
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = kUserRol)]
        public ActionResult Agregar()
        {
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_ArchivoModel model)//TODO: comprobar el Modelo
        {
            SICACI_DAL db = new SICACI_DAL();
            // db.IUsers.CrearUsuario(model.Usuario, model.Nombres, model.Apellidos, model.CorreoE, model.Password, model.Rol);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El " + kItemType + " se ha creado correctamente", titulo: "Nuevo " + kItemType, permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //#region Read

        //[HttpGet()]
        //[JFHandleExceptionMessage(Order = 1)]
        //[Authorize(Roles = "Administrador")]
        //public ActionResult Consultar(int id)
        //{
        //    //Debemos validar que se haya pasado un usuario en la solicitud
        //    if (id == 0)
        //    {
        //        Response.TrySkipIisCustomErrors = true;
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return Json(new
        //        {
        //            notify = new JFNotifySystemMessage("No se ha especificado en la solicitud la tarea que se desea consultar.",
        //                                                titulo: "Consultar un Tarea",
        //                                                permanente: false,
        //                                                tiempo: 5000)
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    return View(new Models.Consultar_TareaModel
        //    {
        //        id = 1,
                
        //    });
        //}
        //#endregion




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar(int id)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el tarea que se desea modificar.",
                                                        titulo: "Modificación de tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(new Models.Modificar_TareaModel
            {
            });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public JsonResult Eliminar(int id)
        {
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido eliminar la tarea debido a que no existe o no se ha especificado ningun archivo",
                                                        titulo: "Eliminación de archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("La Tarea se ha eliminado correctamente.", titulo: "Eliminación de Tarea", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion

    }
}
