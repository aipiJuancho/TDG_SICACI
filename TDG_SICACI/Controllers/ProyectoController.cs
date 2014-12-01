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
            SICACI_DAL db = new SICACI_DAL();
            var dataPolObj = db.IOrganizacion.GetPoliticasObjetivos_Vigentes();
            var dataPoliticas = from politica in dataPolObj
                                group politica by politica.TEXT_POLITICA into politicaGroup
                                select politicaGroup;

            //Preparamos los datos para el control MultipleSelect
            JFMultipleSelect_Data jfMSData = new JFMultipleSelect_Data();
            List<JFMultipleSelect_Data_Headers> jfMSHeaders = new List<JFMultipleSelect_Data_Headers>();
            List<JFMultipleSelect_Data_Items> jfMSItems = new List<JFMultipleSelect_Data_Items>();
            var i = 1;
            foreach (var iPolitca in dataPoliticas)
            {
                jfMSHeaders.Add(new JFMultipleSelect_Data_Headers() { Label = iPolitca.Key, Order = i });
                foreach (var iObjetivo in iPolitca)
                {
                    jfMSItems.Add(new JFMultipleSelect_Data_Items() { Label = iObjetivo.TEXT_OBJETIVO_ACOTADO, Value = iObjetivo.ID_OBJETIVO.ToString(), HeaderOrder = i});
                }
                i++;
            }
            jfMSData.Headers = jfMSHeaders;
            jfMSData.Items = jfMSItems;
            jfMSData.OrderItems = false;

            //Preparamos los datos de los Usuarios Responsables de Aprobación
            List<SelectListItem> arrRespAprobacion = db.IUsers.GetUserList()
                .Where(u => (u.TIPO_ROL.Equals("Director de proyecto") || u.TIPO_ROL.Equals("Administrador")) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();

            //Preparamos los datos de los Usuarios Responsables de Ejecución
            List<SelectListItem> arrRespEjecucion = db.IUsers.GetUserList()
                .Where(u => (u.TIPO_ROL.Equals("Director de proyecto") || u.TIPO_ROL.Equals("Administrador")) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();


            ViewBag.jfMSObjetivos = jfMSData;
            ViewBag.RespAprobacion = arrRespAprobacion;
            ViewBag.RespEjecucion = arrRespEjecucion;
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_ProyectoModel model)//TODO: comprobar el Modelo
        {
            //Antes de almacenar los datos, verificamos si el usuario ha seleccionado al menos un objetivo
            if ((string.IsNullOrWhiteSpace(model.objetivosAsociados)) || (model.objetivosAsociados.Equals("null"))) {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se puede continuar debido a que no ha seleccionado ningun objetivo. Para continuar, por favor seleccione al menos un objetivo de la lista",
                                                        titulo: "Sin objetivo seleccionado",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }
            
            SICACI_DAL db = new SICACI_DAL();
            db.IProyectos.NuevoProyecto(model.nombre, model.responableEjecucion, model.responableAprobacion,
                model.objetivosAsociados, model.findingsAsociados, model.fechaInicio, User.Identity.Name);
            
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
