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
    public class ProyectoController : BaseController
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol = "Administrador";
        private const string kItemType = "Proyecto";
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

            List<Models.Grid_ProyectoViewModel> items = new List<Models.Grid_ProyectoViewModel>()
            {
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 },
               new Models.Grid_ProyectoViewModel { id = 1, nombre ="Nombre del proyecto", responableEjecucion = "Sofy", fechaInicio = DateTime.Today, fechaFinalizacion = DateTime.Today, progreso = 15 }

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
        public JsonResult Agregar(Models.Agregar_ProyectoModel model)//TODO: comprobar el Modelo
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
        #region Read

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Consultar(int id)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el Proyecto que se desea consultar.",
                                                        titulo: "Consultar un Proyecto",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return View(new Models.Consultar_ProyectoModel
            {
                id = 1, 
                nombre ="Nombre del proyecto", 
                responableEjecucion = "Sofy", 
                responableAprobacion = "Juan", 
                objetivosAsociados = "Texto de los objetivos asociados", 
                findingsAsociados ="Texto de los findings asociados", 
                fechaInicio = DateTime.Today, 
                fechaFinalizacion = DateTime.Today, 
                progreso = 15, 
                aprobacion = "Pendiente"
            });
        }
        #endregion




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
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el Proyecto que se desea modificar.",
                                                        titulo: "Modificación de Proyecto",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(new Models.Modificar_ProyectoModel
            {
                id = 1,
                nombre = "Nombre del proyecto",
                responableEjecucion = "Sofy",
                responableAprobacion = "Juan",
                objetivosAsociados = "Texto de los objetivos asociados",
                findingsAsociados = "Texto de los findings asociados",
                fechaInicio = DateTime.Today,
                aprobacion = "Pendiente"
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
                    notify = new JFNotifySystemMessage("No se ha podido eliminar el Proyecto debido a que no existe o no se ha especificado ningun Proyecto",
                                                        titulo: "Eliminación de Proyecto",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El Proyecto se ha eliminado correctamente.", titulo: "Eliminación de Proyecto", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion


    }
}
