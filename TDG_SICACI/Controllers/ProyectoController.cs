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
using System.Globalization;

namespace TDG_SICACI.Controllers
{
    public class ProyectoController : BaseController
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol_All = "Administrador,RD,Responsable Proyecto,Responsable Tarea";
        private const string kUserRol_Admins = "Administrador,RD,Responsable Proyecto";
        private const string kItemType = "Proyecto";
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        public ActionResult Index()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        public JsonResult DataGrid(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();
            IEnumerable<Database.SP_GRID_PROYECTOS_MODEL> data;
            int rowCount;

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            if ((User.IsInRole("Administrador")) || (User.IsInRole("RD")))
            {
                data = (model.sorting != null ?
                    db.IProyectos.GetGridData().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                    db.IProyectos.GetGridData());
                rowCount = db.IProyectos.GetGridData().Count();
            }
            else
            {
                data = (model.sorting != null ?
                    db.IProyectos.GetGridData_ByUser(User.Identity.Name).AsQueryable()
                        .JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                    db.IProyectos.GetGridData_ByUser(User.Identity.Name));
                rowCount = db.IProyectos.GetGridData_ByUser(User.Identity.Name).Count();
            }

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_ProyectoViewModel
                {
                    ID = u.ID,
                    NOMBRE_PROYECTO = u.NOMBRE_PROYECTO,
                    RESP_EJECUCION = u.RESP_EJECUCION,
                    ESTADO_PROYECTO = u.ESTADO_PROYECTO,
                    FECHA_INICIO = u.FECHA_INICIO.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
                    FECHA_FINALIZACION = (u.FECHA_FINALIZACION.HasValue ? u.FECHA_FINALIZACION.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty),
                    PROGRESO = string.Format("{0}%", u.PROGRESO_PROYECTO)
                });

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = rowCount,
                page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Create
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_Admins)]
        [JFUnathorizedJSONResult()]
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
                .Where(u => ((u.TIPO_ROL.Equals("Administrador")) || (u.TIPO_ROL.Equals("RD"))) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();

            //Preparamos los datos de los Usuarios Responsables de Ejecución
            List<SelectListItem> arrRespEjecucion = db.IUsers.GetUserList()
                .Where(u => (u.TIPO_ROL.Equals("RD") || u.TIPO_ROL.Equals("Administrador") || u.TIPO_ROL.Equals("Responsable Proyecto")) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();

            //Preparamos los datos para los findings
            JFMultipleSelect_Data jfMSfindings = new JFMultipleSelect_Data();
            List<JFMultipleSelect_Data_Items> jfMSfindingsItems = db.IFindings.GetAll()
                .Where(f => (f.ESTADO.Equals("Pendiente")))
                .Select(f => new JFMultipleSelect_Data_Items()
                {
                    Label = (f.COMENTARIO.Length > 80 ? string.Format("{0}...", f.COMENTARIO.Substring(0, 80)) : f.COMENTARIO),
                    Value = f.ID.ToString(),
                    SubText = string.Format("{0} - {1}", f.TIPO_NOCONFORMIDAD, f.TIPO_CORRECION)
                }).ToList();
            jfMSfindings.Items = jfMSfindingsItems;
            ViewBag.Findings = jfMSfindings;

            ViewBag.jfMSObjetivos = jfMSData;
            ViewBag.RespAprobacion = arrRespAprobacion;
            ViewBag.RespEjecucion = arrRespEjecucion;
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol_Admins)]
        [JFUnathorizedJSONResult()]
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
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        public ActionResult Consultar(int id = 0)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.ErrorMessage = "ERROR! No se especifico en la solicitud el Proyecto que se desea consultar.";
                return View("Error");
            }

            var db = new SICACI_DAL();
            if  (!((User.IsInRole("Administrador")) || (User.IsInRole("RD"))))
            {
                if (db.IProyectos.GetGridData_ByUser(User.Identity.Name)
                        .Where(p => p.ID.Equals(id)).Count().Equals(0))
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    ViewBag.ErrorMessage = "Lo sentimos, pero no se encontro en el sistema el proyecto especificado.";
                    return View("Error");
                }
            }

            var proyecto = db.IProyectos.Consultar().Where(p => p.ID.Equals(id)).FirstOrDefault();

            //Validamos si se encontro el proyecto en el sistema
            if (proyecto == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = "Lo sentimos, pero no se encontro en el sistema el proyecto especificado.";
                return View("Error");
            }

            return View(new Models.Consultar_ProyectoModel
            {

                id = proyecto.ID, 
                nombre = proyecto.NOMBRE_PROYECTO, 
                responableEjecucion = proyecto.RESPONSABLE_EJECUCION, 
                responableAprobacion = proyecto.RESPONSABLE_APROBACION, 
                objetivosAsociados = db.IProyectos.ConsultarObjetivosProyecto()
                    .Where(o => o.ID_PROYECTO.Equals(id))
                    .Select(o => o.TEXT_OBJETIVO)
                    .ToList(),
                progreso = proyecto.PROGRESO_PROYECTO.ToString(),
                findingsAsociados = db.IFindings.GetFindings_Proyecto()
                    .Where(f => f.ID_PROYECTO.Equals(id))
                    .Select(f => new Models.FindingAsociado() {
                        id = f.ID_FINDING,
                        comentario = f.COMENTARIO,
                        estado = f.ESTADO
                    }).ToList(),
                fechaInicio = proyecto.FECHA_INICIO.ToString("dd/MM/yyyy"), 
                fechaFinalizacion = (proyecto.FECHA_FINALIZACION.HasValue ? proyecto.FECHA_FINALIZACION.Value.ToString("dd/MM/yyyy") : "Sin fecha de finalización"),
                aprobacion = proyecto.ESTADO_PROYECTO,
                CreadorProyecto = proyecto.CREADOR_PROYECTO,
                FechaCreacion = proyecto.FECHA_CREACION_PROYECTO.Value.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US"))
            });
        }
        #endregion




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_Admins)]
        [JFUnathorizedJSONResult()]
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

            var db = new SICACI_DAL();
            var proyecto = db.IProyectos.Consultar().Where(p => p.ID.Equals(id)).FirstOrDefault();

            //Validamos si se encontro el proyecto en el sistema
            if (proyecto == null) {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("Lo sentimos, pero no se encontro en el sistema el proyecto especificado.",
                                                        titulo: "Modificación de Proyecto",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            //Validamos si el proyecto se encuentra en estado PENDIENTE
            if (!proyecto.ID_ESTADO_PROYECTO.Equals("PE"))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var msj = string.Format("El proyecto \"{0}\" no puede ser modificado debido a que el proyecto se encuentra en estado {1}.", proyecto.NOMBRE_PROYECTO.ToUpper(), proyecto.ESTADO_PROYECTO.ToUpper());

                return Json(new
                {
                    notify = new JFNotifySystemMessage(msj,
                                                        titulo: "Modificación de Proyecto", 
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            //Preparamos los datos de los Usuarios Responsables de Aprobación
            List<SelectListItem> arrRespAprobacion = db.IUsers.GetUserList()
                .Where(u => ((u.TIPO_ROL.Equals("Administrador")) || (u.TIPO_ROL.Equals("RD"))) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();

            //Preparamos los datos de los Usuarios Responsables de Ejecución
            List<SelectListItem> arrRespEjecucion = db.IUsers.GetUserList()
                .Where(u => (u.TIPO_ROL.Equals("RD") || u.TIPO_ROL.Equals("Administrador") || u.TIPO_ROL.Equals("Responsable Proyecto")) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();

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
                    jfMSItems.Add(new JFMultipleSelect_Data_Items() { Label = iObjetivo.TEXT_OBJETIVO_ACOTADO, Value = iObjetivo.ID_OBJETIVO.ToString(), HeaderOrder = i });
                }
                i++;
            }
            jfMSData.Headers = jfMSHeaders;
            jfMSData.Items = jfMSItems;
            jfMSData.OrderItems = false;

            //Preparamos los datos para los findings
            JFMultipleSelect_Data jfMSfindings = new JFMultipleSelect_Data();
            List<JFMultipleSelect_Data_Items> jfMSfindingsItems = db.IFindings.GetAll()
                .Where(f => (f.ESTADO.Equals("Pendiente")))
                .Select(f => new JFMultipleSelect_Data_Items()
                {
                    Label = (f.COMENTARIO.Length > 80 ? string.Format("{0}...", f.COMENTARIO.Substring(0, 80)) : f.COMENTARIO),
                    Value = f.ID.ToString(),
                    SubText = string.Format("{0} - {1}", f.TIPO_NOCONFORMIDAD, f.TIPO_CORRECION)
                }).ToList();
            jfMSfindings.Items = jfMSfindingsItems;
            ViewBag.Findings = jfMSfindings;


            //Cargamos los objetivos que se habian seleccionado cuando se creo el proyecto
            var objSelected = db.IProyectos.ConsultarObjetivosProyecto()
                .Where(p => p.ID_PROYECTO.Equals(id))
                .Select(o => o.ID_OBJETIVO)
                .ToArray();

            var objFindingsSelected = db.IFindings.GetFindings_Proyecto()
                .Where(f => f.ID_PROYECTO.Equals(id))
                .Select(f => f.ID_FINDING)
                .ToArray();

            //Preparamos todos los parametros que se van enviar a la VIEW
            ViewBag.jfMSObjetivos = jfMSData;
            ViewBag.RespAprobacion = arrRespAprobacion;
            ViewBag.RespEjecucion = arrRespEjecucion;
            ViewBag.ObjetivosSelected = objSelected.Select(o => o.ToString()).ToArray();
            ViewBag.FindingsSelected = objFindingsSelected.Select(o => o.ToString()).ToArray();

            return PartialView(new Models.Modificar_ProyectoModel
            {
                id = proyecto.ID,
                nombre = proyecto.NOMBRE_PROYECTO,
                responableEjecucion = proyecto.ID_RESP_EJECUCION,
                responableAprobacion = proyecto.ID_RESP_APROBACION,
                objetivosAsociados = string.Join(",", objSelected),
                findingsAsociados = "Sin Información",
                fechaInicio = proyecto.FECHA_INICIO,
                aprobacion = proyecto.ID_ESTADO_PROYECTO
            });
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol_Admins)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Modificar(Models.Modificar_ProyectoModel model)//TODO: comprobar el Modelo
        {
            //Antes de almacenar los datos, verificamos si el usuario ha seleccionado al menos un objetivo
            if ((string.IsNullOrWhiteSpace(model.objetivosAsociados)) || (model.objetivosAsociados.Equals("null")))
            {
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
            db.IProyectos.ModificarProyecto(model.id, model.nombre, model.responableEjecucion, model.responableAprobacion,
                model.objetivosAsociados, model.findingsAsociados, model.fechaInicio, User.Identity.Name, model.aprobacion);

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El " + kItemType + " se ha modificado correctamente", titulo: "Modificación del " + kItemType, permanente: true, icono: JFNotifySystemIcon.Update)
            });
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_Admins)]
        [JFUnathorizedJSONResult()]
        public JsonResult Eliminar(int id = 0)
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

            var db = new SICACI_DAL();
            db.IProyectos.EliminarProyecto(id);

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El Proyecto se ha eliminado correctamente.", titulo: "Eliminación de Proyecto", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion


    }
}
