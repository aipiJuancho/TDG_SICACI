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
        public ActionResult Index(int id)
        {
            //Validamos que el codigo de proyecto exista en el sistema
            SICACI_DAL db = new SICACI_DAL();
            if (db.IProyectos.Consultar().Where(p => p.ID.Equals(id)).Count().Equals(0))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.ErrorMessage = "ERROR! El código de proyecto especificado no existe en el sistema o usted no tiene permisos para acceder al proyecto";
                return View("Error");
            }

            if (User.IsInRole(kUserRol))
            {
                ViewBag.projectId = id.ToString();// este id es el id del proyecto
                return View();
            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        [Authorize(Roles = kUserRol)]
        public JsonResult DataGrid(jfBSGrid_Respond model, int IDProyecto)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IProyectos.GetTareas().Where(p => p.ID_PROYECTO.Equals(IDProyecto)).AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                    db.IProyectos.GetTareas().Where(p => p.ID_PROYECTO.Equals(IDProyecto)));

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_TareaViewModel
                {
                    ID_PROYECTO = u.ID_PROYECTO,
                    ID_TAREA = u.ID_TAREA,
                    TITULO_TAREA = u.TITULO_TAREA,
                    RESPONSABLE_EJECUCION = u.RESPONSABLE_EJECUCION,
                    PROGRESO = Math.Round((u.PROGRESO * 100),0).ToString() + '%',
                    ORDEN = u.ORDEN.Value,
                    FECHA_FINALIZACION = (u.FECHA_FINALIZACION.HasValue ? u.FECHA_FINALIZACION.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty)
                });

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = db.IProyectos.GetTareas().Where(p => p.ID_PROYECTO.Equals(IDProyecto)).Count(),
                page_data = dataUsers
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
            var db = new SICACI_DAL();

            //Preparamos los datos de los Usuarios Responsables de Ejecución
            List<SelectListItem> arrResponsable = db.IUsers.GetUserList()
                .Where(u => (u.TIPO_ROL.Equals("Director de proyecto") || u.TIPO_ROL.Equals("Administrador") || u.TIPO_ROL.Equals("Responsable Tarea") || u.TIPO_ROL.Equals("Responsable Proyecto")) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO
                }).ToList();
            ViewBag.Responsables = arrResponsable;

            List<SelectListItem> arrProgreso = new List<SelectListItem>() {
                new SelectListItem() {Text = "0% completado", Value="0.00", Selected = true},
                new SelectListItem() {Text = "5% completado", Value="0.05"},
                new SelectListItem() {Text = "10% completado", Value="0.10"},
                new SelectListItem() {Text = "15% completado", Value="0.15"},
                new SelectListItem() {Text = "20% completado", Value="0.20"},
                new SelectListItem() {Text = "25% completado", Value="0.25"},
                new SelectListItem() {Text = "30% completado", Value="0.30"},
                new SelectListItem() {Text = "35% completado", Value="0.35"},
                new SelectListItem() {Text = "40% completado", Value="0.40"},
                new SelectListItem() {Text = "45% completado", Value="0.45"},
                new SelectListItem() {Text = "50% completado", Value="0.50"},
                new SelectListItem() {Text = "55% completado", Value="0.55"},
                new SelectListItem() {Text = "60% completado", Value="0.60"},
                new SelectListItem() {Text = "65% completado", Value="0.65"},
                new SelectListItem() {Text = "70% completado", Value="0.70"},
                new SelectListItem() {Text = "75% completado", Value="0.75"},
                new SelectListItem() {Text = "80% completado", Value="0.80"},
                new SelectListItem() {Text = "85% completado", Value="0.85"},
                new SelectListItem() {Text = "90% completado", Value="0.90"},
                new SelectListItem() {Text = "95% completado", Value="0.95"},
                new SelectListItem() {Text = "100% completado", Value="1.00"}
            };
            ViewBag.Progreso = arrProgreso;

            //Preparamos los datos para el control MultipleSelect
            JFMultipleSelect_Data jfMSData = new JFMultipleSelect_Data();
            List<JFMultipleSelect_Data_Items> jfMSItems = db.IUsers.GetUserList()
                .Where(u => (u.ACTIVO.Equals("Activo")))
                .Select(u => new JFMultipleSelect_Data_Items()
                {
                    Label = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO,
                    SubText = u.TIPO_ROL
                }).ToList();
            jfMSData.Items = jfMSItems;
            ViewBag.Personal = jfMSData;

            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_TareaModel model, int IDProyecto)
        {
            //Antes de almacenar los datos, verificamos si el usuario ha seleccionado al menos un objetivo
            if ((string.IsNullOrWhiteSpace(model.personasInvolucradas)) || (model.personasInvolucradas.Equals("null")))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se puede continuar debido a que no ha seleccionado ninguna persona involucrada en el proyecto. Para continuar, por favor seleccione al menos un usuario de la lista",
                                                        titulo: "Sin Personal Involucrado",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            var dProgreso = (decimal.Parse(model.progreso) / 100);
            db.IProyectos.CrearTarea(IDProyecto, model.orden, model.titulo, model.descripcion, model.responableEjecucion,
                model.recursosAsignados, model.fechaFin, dProgreso, model.personasInvolucradas,
                User.Identity.Name);

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
        public ActionResult Modificar(int ID_TAREA)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (ID_TAREA == 0)
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

            var db = new SICACI_DAL();
            var info = db.IProyectos.ConsultarInfo_Tarea(ID_TAREA);
            if (info == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido recuperar la información de la tarea seleccionada.",
                                                        titulo: "Modificación de tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            //Empezamos a construir la información de los ComboBox e Items de la vista
            List<SelectListItem> arrResponsable = db.IUsers.GetUserList()
                .Where(u => (u.TIPO_ROL.Equals("Director de proyecto") || u.TIPO_ROL.Equals("Administrador") || u.TIPO_ROL.Equals("Responsable Tarea") || u.TIPO_ROL.Equals("Responsable Proyecto")) && u.ACTIVO.Equals("Activo"))
                .Select(u => new SelectListItem()
                {
                    Text = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO,
                    Selected = (u.USUARIO.Equals(info.RESPONSABLE) ? true : false)
                }).ToList();
            ViewBag.Responsables = arrResponsable;

            List<SelectListItem> arrProgreso = new List<SelectListItem>() {
                new SelectListItem() {Text = "0% completado", Value="0.00"},
                new SelectListItem() {Text = "5% completado", Value="0.05"},
                new SelectListItem() {Text = "10% completado", Value="0.10"},
                new SelectListItem() {Text = "15% completado", Value="0.15"},
                new SelectListItem() {Text = "20% completado", Value="0.20"},
                new SelectListItem() {Text = "25% completado", Value="0.25"},
                new SelectListItem() {Text = "30% completado", Value="0.30"},
                new SelectListItem() {Text = "35% completado", Value="0.35"},
                new SelectListItem() {Text = "40% completado", Value="0.40"},
                new SelectListItem() {Text = "45% completado", Value="0.45"},
                new SelectListItem() {Text = "50% completado", Value="0.50"},
                new SelectListItem() {Text = "55% completado", Value="0.55"},
                new SelectListItem() {Text = "60% completado", Value="0.60"},
                new SelectListItem() {Text = "65% completado", Value="0.65"},
                new SelectListItem() {Text = "70% completado", Value="0.70"},
                new SelectListItem() {Text = "75% completado", Value="0.75"},
                new SelectListItem() {Text = "80% completado", Value="0.80"},
                new SelectListItem() {Text = "85% completado", Value="0.85"},
                new SelectListItem() {Text = "90% completado", Value="0.90"},
                new SelectListItem() {Text = "95% completado", Value="0.95"},
                new SelectListItem() {Text = "100% completado", Value="1.00"}
            };
            ViewBag.Progreso = arrProgreso;

            //Preparamos los datos para el control MultipleSelect
            JFMultipleSelect_Data jfMSData = new JFMultipleSelect_Data();
            List<JFMultipleSelect_Data_Items> jfMSItems = db.IUsers.GetUserList()
                .Where(u => (u.ACTIVO.Equals("Activo")))
                .Select(u => new JFMultipleSelect_Data_Items()
                {
                    Label = string.Format("{0} {1}", u.NOMBRES, u.APELLIDOS),
                    Value = u.USUARIO,
                    SubText = u.TIPO_ROL
                }).ToList();
            jfMSData.Items = jfMSItems;
            ViewBag.Personal = jfMSData;

            //Cargamos el personal involucrado que se habian seleccionado cuando se creo la tarea
            var perSelected = db.IProyectos.ConsultarPersonal_Tarea(ID_TAREA)
                .Select(t => t.ID_PERSONA)
                .ToArray();
            ViewBag.PersonalSelected = perSelected.Select(o => o.ToString()).ToArray();


            return PartialView(new Models.Modificar_TareaModel
            {
                orden = info.ORDEN_VISUAL.Value,
                titulo = info.TITULO,
                descripcion = info.DESCRIPCION,
                responableEjecucion = info.ID_RESPONSABLE,
                recursosAsignados = info.RECURSOS_ASIGNADOS,
                fechaFin = info.FECHA_FIN_PREVISTA.Value,
                progreso = (info.PROGRESO.HasValue ? info.PROGRESO.Value.ToString("0.00", new CultureInfo("en-US")) : "0.00"),
                personasInvolucradas = string.Join(",", perSelected),
                cantidadArchivosAdjuntos = 99,
                cantidadComentarios = 88
            });
        }




        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = kUserRol)]
        public ActionResult _newModal_Files()
        {
            //return PartialView();
            return PartialView(new Models.agregarArchivoAdjunto
            {
                
                archivos = new List<Models.archivoAdjunto> 
                                    {
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"},
                                        new Models.archivoAdjunto { nombre = "nombre del archivo", url = "url"}
                                    }


            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = kUserRol)]
        public ActionResult _newModal_Comments()
        {
            return PartialView(new Models.agregarComentario
            {

                comentarios = new List<Models.comentario>
                                    {
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"},
                                        new Models.comentario { usuario = "juan", texto= "texto del comentario"}
                                    },


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
