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
using System.IO;

namespace TDG_SICACI.Controllers
{
    public class TareaController : BaseController
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol_All = "Administrador,RD,Responsable Proyecto,Responsable Tarea";
        private const string kItemType = "Tarea";
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        public ActionResult Index(int id, int idTarea = 0)
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

            bool showButtons = true;
            if (db.IProyectos.Consultar().Where(p => p.ID.Equals(id) && p.ID_ESTADO_PROYECTO.Equals("FI")).Count().Equals(1))
                showButtons = false;
            ViewBag.ShowButtons = showButtons;

            //Verificamos si debemos de abrir el MODAL de manera automatica
            int openModal = 0;
            if ((idTarea > 0) && (db.IProyectos.ConsultarTareas()
                    .Where(t => t.ID_PROYECTO.Equals(id) && t.ID_TAREA.Equals(idTarea))
                    .Count().Equals(1)))
                openModal = idTarea;
            ViewBag.OpenModal = openModal;

            ViewBag.projectId = id.ToString();// este id es el id del proyecto
            return View();
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
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
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
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
                new SelectListItem() {Text = "0% completado", Value="0", Selected = true},
                new SelectListItem() {Text = "5% completado", Value="5"},
                new SelectListItem() {Text = "10% completado", Value="10"},
                new SelectListItem() {Text = "15% completado", Value="15"},
                new SelectListItem() {Text = "20% completado", Value="20"},
                new SelectListItem() {Text = "25% completado", Value="25"},
                new SelectListItem() {Text = "30% completado", Value="30"},
                new SelectListItem() {Text = "35% completado", Value="35"},
                new SelectListItem() {Text = "40% completado", Value="40"},
                new SelectListItem() {Text = "45% completado", Value="45"},
                new SelectListItem() {Text = "50% completado", Value="50"},
                new SelectListItem() {Text = "55% completado", Value="55"},
                new SelectListItem() {Text = "60% completado", Value="60"},
                new SelectListItem() {Text = "65% completado", Value="65"},
                new SelectListItem() {Text = "70% completado", Value="70"},
                new SelectListItem() {Text = "75% completado", Value="75"},
                new SelectListItem() {Text = "80% completado", Value="80"},
                new SelectListItem() {Text = "85% completado", Value="85"},
                new SelectListItem() {Text = "90% completado", Value="90"},
                new SelectListItem() {Text = "95% completado", Value="95"},
                new SelectListItem() {Text = "100% completado", Value="100"}
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
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
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
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
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
                new SelectListItem() {Text = "0% completado", Value="0"},
                new SelectListItem() {Text = "5% completado", Value="5"},
                new SelectListItem() {Text = "10% completado", Value="10"},
                new SelectListItem() {Text = "15% completado", Value="15"},
                new SelectListItem() {Text = "20% completado", Value="20"},
                new SelectListItem() {Text = "25% completado", Value="25"},
                new SelectListItem() {Text = "30% completado", Value="30"},
                new SelectListItem() {Text = "35% completado", Value="35"},
                new SelectListItem() {Text = "40% completado", Value="40"},
                new SelectListItem() {Text = "45% completado", Value="45"},
                new SelectListItem() {Text = "50% completado", Value="50"},
                new SelectListItem() {Text = "55% completado", Value="55"},
                new SelectListItem() {Text = "60% completado", Value="60"},
                new SelectListItem() {Text = "65% completado", Value="65"},
                new SelectListItem() {Text = "70% completado", Value="70"},
                new SelectListItem() {Text = "75% completado", Value="75"},
                new SelectListItem() {Text = "80% completado", Value="80"},
                new SelectListItem() {Text = "85% completado", Value="85"},
                new SelectListItem() {Text = "90% completado", Value="90"},
                new SelectListItem() {Text = "95% completado", Value="95"},
                new SelectListItem() {Text = "100% completado", Value="100"}
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

            //Enviamos de vuelta a la vista el ID
            ViewBag.ID = ID_TAREA;
            int _progreso = (int) (info.PROGRESO.Value * 100);

            return PartialView(new Models.Modificar_TareaModel
            {
                orden = info.ORDEN_VISUAL.Value,
                titulo = info.TITULO,
                descripcion = info.DESCRIPCION,
                responableEjecucion = info.ID_RESPONSABLE,
                recursosAsignados = info.RECURSOS_ASIGNADOS,
                fechaFin = info.FECHA_FIN_PREVISTA.Value,
                progreso = _progreso.ToString(),
                personasInvolucradas = string.Join(",", perSelected),
                cantidadArchivosAdjuntos = 99,
                cantidadComentarios = 88
            });
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Modificar(Models.Modificar_TareaModel model, int idTarea)
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
            db.IProyectos.ModificarTarea(idTarea, model.orden, model.titulo, model.descripcion, model.responableEjecucion,
                model.recursosAsignados, model.fechaFin, dProgreso, model.personasInvolucradas, User.Identity.Name);
           
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El " + kItemType + " se ha modificado correctamente", titulo: "Modificación del " + kItemType, permanente: true, icono: JFNotifySystemIcon.Update)
            });
        }


        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        public ActionResult _newModal_Files(int id = 0)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el ID de la tarea.",
                                                        titulo: "Modificación de tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            var db = new SICACI_DAL();
            var info = db.IProyectos.ConsultarInfo_Tarea(id);
            if (info == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido recuperar la información de la tarea seleccionada ya que el ID especificado en la solicitu no existe.",
                                                        titulo: "Modificación de tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            //Recuperamos todos los arhivos que esten asociados a esta tarea.
            var files = db.IProyectos.ConsultarArchivos_Tarea(id).OrderByDescending(f => f.FECHA_SUBIDA)
                .Select(f => new Models.archivoAdjunto()
                {
                    nombre = f.TITULO_ARCHIVO,
                    url = string.Format("{0}?file={1}", Url.Action("ver_documento"), f.NOMBRE_ARCHIVO),
                    fechaCreacion = f.FECHA_SUBIDA.Value.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")),
                    usuario = f.NOMBRE_USUARIO,
                    fileName = f.NOMBRE_ARCHIVO
                }).ToList();

            ViewBag.ID = id;
            return PartialView(new Models.agregarArchivoAdjunto {archivos = files});
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _adjuntarArchivo(Models.agregarArchivoAdjunto model, int id = 0)
        {
            //Validamos que el documento sea correcto
            if ((model.documento == null) || model.documento.ContentLength <= 0)
            {
                return Json(new
                {
                    success = false,
                    notify = new JFNotifySystemMessage("Lo sentimos, pero no se adjunto ningun documento o el formato no es válido.",
                                                        titulo: "Adjuntar documento a tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                });
            }

            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el ID de la tarea.",
                                                        titulo: "Modificación de tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            var strFileName = db.IProyectos.VincularArchivo_Tarea(id, model.nombre,
                model.documento.FileName.Split('.').LastOrDefault(), User.Identity.Name);
            
            //Guardamos el archivo fisicamente en el servidor
            var path = Path.Combine(Server.MapPath("~/App_Data/tareas"), strFileName);
            model.documento.SaveAs(path);

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Se ha vinculado correctamente el archivo a la tarea.", titulo: "Documento adjuntado", permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _deleteFileTarea (string filename = "", int id = 0)
        {
            //validamos los datos que vengan correctos
            if ((id == 0) || (string.IsNullOrWhiteSpace(filename)))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el ID o el nombre del archivo de la tarea.",
                                                        titulo: "Eliminación de arhicvo en tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            db.IProyectos.EliminarArchivo_Tarea(id, filename, User.Identity.Name);

            //Una vez el archivo ha sido eliminado de la base, lo eliminamos fisicamente del HDD
            var path = Path.Combine(Server.MapPath("~/App_Data/tareas"), filename);
            System.IO.File.Delete(path);

            return Json(new
            {
                success = true,
                ID = filename.Split('.').FirstOrDefault(),
                notify = new JFNotifySystemMessage("Se elimino correctamente el archivo de la tarea.", titulo: "Documento Eliminado", permanente: false, icono: JFNotifySystemIcon.Update)
            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        public ActionResult ver_documento(string file)
        {
            string path = Path.Combine(Server.MapPath("~/App_Data/tareas"), file);

            //Verificamos si existe el archivo en el sistema
            if (!System.IO.File.Exists(path))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = "Lo sentimos, pero no se encontro el archivo especificado";
                return View("Error");
            }

            //Regresamos el archivo PDF especificado en el gestor de documentos
            Response.AppendHeader("Content-Disposition", string.Format("inline; filename={0}", file));
            return File(path, "application/pdf");
        }


        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        public ActionResult _newModal_Comments(int id = 0)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el ID de la tarea.",
                                                        titulo: "Comentarios de tareas",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            var db = new SICACI_DAL();
            var info = db.IProyectos.ConsultarInfo_Tarea(id);
            if (info == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido recuperar la información de la tarea seleccionada ya que el ID especificado en la solicitu no existe.",
                                                        titulo: "Comentarios de tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            //Recuperamos todos los arhivos que esten asociados a esta tarea.
            var comments = db.IProyectos.ConsultarComentarios_Tarea(id).OrderBy(f => f.ORDEN)
                .Select(c => new Models.comentario
                {
                   texto = c.COMENTARIO,
                   usuario = c.NOMBRE_USUARIO,
                   fechaComentario = c.FECHA_COMENTARIO.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")),
                   id = c.ID_COMENTARIO
                }).ToList();

            ViewBag.ID = id;
            return PartialView(new Models.agregarComentario{comentarios = comments});
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _agregarComentario(Models.agregarComentario model, int id = 0)
        {
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    success = false,
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el ID de la tarea.",
                                                        titulo: "Añadir comentario a tarea",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            db.IProyectos.CrearComentario_Tarea(id, model.texto, User.Identity.Name);
            
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Se ha guardado el comentario correctamente.", titulo: "Comentario añadido", permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpGet()]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
        [JFUnathorizedJSONResult()]
        public ActionResult _controlsModificar(int id = 0)
        {
            ViewBag.ID = id;

            var db = new SICACI_DAL();
            ViewBag.countArchivos = db.IProyectos.ConsultarArchivos_Tarea(id).Count();
            ViewBag.countComentarios = db.IProyectos.ConsultarComentarios_Tarea(id).Count();
            return PartialView();
        }


        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol_All)]
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
                    notify = new JFNotifySystemMessage("No se ha podido eliminar la tarea debido a que no existe o no se ha especificado ningun archivo",
                                                        titulo: "Eliminación de archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            var db = new SICACI_DAL();
            var files = db.IProyectos.ConsultarArchivos_Tarea(id).ToArray();    //Almacenamos los archivos que posterior debemos borrar
            db.IProyectos.EliminarTarea(id, User.Identity.Name);    //borramos la tarea de la base

            //Si la tarea fue eliminada satisfactoriamente, borramos los archivos fisicos del disco.
            string path;
            foreach (var file in files)
            {
                path = Path.Combine(Server.MapPath("~/App_Data/tareas"), file.NOMBRE_ARCHIVO);
                System.IO.File.Delete(path);
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
