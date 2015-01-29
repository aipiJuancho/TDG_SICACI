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
using System.IO;
using System.Globalization;

namespace TDG_SICACI.Controllers
{
    public class ArchivoController : BaseController
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol   = "Administrador,RD";
        private const string kItemType  = "Archivo";   
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        public JsonResult DataGrid(jfBSGrid_Respond model)
        {
            SICACI_DAL db = new SICACI_DAL();
            var items = db.IArchivos.Grid_FileGroups()
                .Select(f => new Models.Grid_ArchivoViewModel()
                {
                    ID_FILEGROUP = f.ID_FILEGROUP,
                    FILEGROUP_NAME = f.FILEGROUP_NAME,
                    ETIQUETA = f.ETIQUETA,
                    FECHA_ULTIMA_VERSION = f.FECHA_ULTIMA_VERSION.Value.ToString("dd \\de MMMM \\del yyyy hh:mm tt", new CultureInfo("es-SV")),
                    VERSIONES = f.VERSIONES.Value
                }).ToList();

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = items.Count(),
                page_data = items
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        [JFUnathorizedJSONResult()]
        public JsonResult _get_filegroups(int idSelect = 0)
        {
            SICACI_DAL db = new SICACI_DAL();
            var fileGroups = db.IArchivos.Get_FileGroups()
                .Select(f => new SelectListItem() { Text = f.FILEGROUP_NAME, Value = f.ID_FILEGROUP.ToString()})
                .ToList();

            /*Verificamos si se ha definido algun item a seleccionar al momento de cargar*/
            if (idSelect > 0 ) 
                return Json(new {Items = fileGroups, lastItem = idSelect}, JsonRequestBehavior.AllowGet);
            else
                return Json(new JFComboboxToJSON(fileGroups), JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult _versionamiento()
        {
            return PartialView();
        }

        [HttpPost()]
        public JsonResult _grid_versions(int id, jfBSGrid_Respond model)
        {
            SICACI_DAL db = new SICACI_DAL();
            var items = db.IArchivos.Get_Versions_ByFilegroup(id)
                .Select(f => new Models.Grid_VersionFilegroup
                {
                    NO_VERSION = (int)f.NO_VERSION,
                    ETIQUETA = f.ETIQUETA,
                    FECHA_CREACION = f.FECHA_CREACION.ToString("dd \\de MMMM \\del yyyy hh:mm tt", new CultureInfo("es-SV")),
                    USUARIO = f.USUARIO
                }).ToList();

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
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public ActionResult Agregar()
        {
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_ArchivoModel model)//TODO: comprobar el Modelo
        {
            SICACI_DAL db = new SICACI_DAL();
            var strFileName = db.IArchivos.Create_Version_FileGroup(int.Parse(model.nombre), User.Identity.Name, 
                model.etiqueta, model.documento.FileName.Split('.').LastOrDefault());

            /*Verificamos si se ha cargado algun archivo*/
            if (model.documento != null)
            {
                if (model.documento.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/App_Data/filemanager"), strFileName);
                    model.documento.SaveAs(path);
                }
            }
            
            
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El "+kItemType+" se ha creado correctamente", titulo: "Nuevo "+kItemType, permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        public ActionResult _newModal_FileGroup()
        {
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        [JFUnathorizedJSONResult()]
        public JsonResult _crear_filegroup_name(Models.New_FileGroupName model)
        {
            SICACI_DAL db = new SICACI_DAL();
            var id = db.IArchivos.Create_FileGroup_Name(model.nombre);
            return Json(new { success = true, ID = id});
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Read
        
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Consultar(int ID_FILEGROUP)
        {
            SICACI_DAL db = new SICACI_DAL();
            var file = db.IArchivos.Get_FileGroup_Last(ID_FILEGROUP);
            string path = Path.Combine(Server.MapPath("~/App_Data/filemanager"), file.ARCHIVO);

            //Verificamos si existe el archivo en el sistema
            if (!System.IO.File.Exists(path))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = "Lo sentimos, pero no se encontro el archivo especificado";
                return View("Error");
            }

            //Regresamos el archivo PDF especificado en el gestor de documentos
            Response.AppendHeader("Content-Disposition", string.Format("inline; filename={0}", file.ARCHIVO));
            string mimeType = string.Empty;
            switch (file.ARCHIVO.Split('.').LastOrDefault().ToUpper())
            {
                case "PDF":
                    mimeType = "application/pdf";
                    break;
                case "JPG":
                    mimeType = "image/jpeg";
                    break;
                case "PNG":
                    mimeType = "image/png";
                    break;
                case "GIF":
                    mimeType = "image/gif";
                    break;
            }
            return File(path, mimeType);
        }

        [HttpGet()]
        public ActionResult _visualizar_byversion(int ID_FILEGROUP, int NO_VERSION)
        {
            try
            {
                SICACI_DAL db = new SICACI_DAL();
                var file = db.IArchivos.Get_FileGroup_ByVersion(ID_FILEGROUP, NO_VERSION);

                string path = Path.Combine(Server.MapPath("~/App_Data/filemanager"), file.ARCHIVO);

                //Verificamos si existe el archivo en el sistema
                if (!System.IO.File.Exists(path))
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    ViewBag.ErrorMessage = "Lo sentimos, pero no se encontro el archivo especificado";
                    return View("Error");
                }

                //Regresamos el archivo PDF especificado en el gestor de documentos
                Response.AppendHeader("Content-Disposition", string.Format("inline; filename={0}", file.ARCHIVO));
                string mimeType = string.Empty;
                switch (file.ARCHIVO.Split('.').LastOrDefault().ToUpper())
                {
                    case "PDF":
                        mimeType = "application/pdf";
                        break;
                    case "JPG":
                        mimeType = "image/jpeg";
                        break;
                    case "PNG":
                        mimeType = "image/png";
                        break;
                    case "GIF":
                        mimeType = "image/gif";
                        break;
                }
                return File(path, mimeType);
            }
            catch (Exception ex)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpGet()]
        public ActionResult _descargar_byversion(int ID_FILEGROUP, int NO_VERSION)
        {
            try
            {
                SICACI_DAL db = new SICACI_DAL();
                var file = db.IArchivos.Get_FileGroup_ByVersion(ID_FILEGROUP, NO_VERSION);

                string path = Path.Combine(Server.MapPath("~/App_Data/filemanager"), file.ARCHIVO);

                //Verificamos si existe el archivo en el sistema
                if (!System.IO.File.Exists(path))
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    ViewBag.ErrorMessage = "Lo sentimos, pero no se encontro el archivo especificado";
                    return View("Error");
                }

                //Regresamos el archivo PDF especificado en el gestor de documentos
                string mimeType = string.Empty;
                switch (file.ARCHIVO.Split('.').LastOrDefault().ToUpper())
                {
                    case "PDF":
                        mimeType = "application/pdf";
                        break;
                    case "JPG":
                        mimeType = "image/jpeg";
                        break;
                    case "PNG":
                        mimeType = "image/png";
                        break;
                    case "GIF":
                        mimeType = "image/gif";
                        break;
                }

                return File(path, mimeType,
                    string.Format("{0}_version{1}.{2}", file.NAME.Replace(" ", "_"), 
                        NO_VERSION.ToString(), file.ARCHIVO.Split('.').LastOrDefault()));
            }
            catch (Exception ex)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public JsonResult _set_primary_version_filegroup(int ID_FILEGROUP, int NO_VERSION)
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IArchivos.Update_PrimaryVersion_Filegroup(ID_FILEGROUP, NO_VERSION);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Se ha cambiado la versión primaria de este documento correctamente", titulo: "Cambio de versión primaria",icono: JFNotifySystemIcon.Update)
            });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        public ActionResult Modificar(string nombre)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el usuario que se desea modificar.",
                                                        titulo: "Modificación de Usuario",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }


            

            return PartialView(new Models.Modificar_ArchivoModel
            {
                nombre              = "nombre del archivo",
                etiqueta            = "etiqueta del archivo",
                archivosVersionados = new List<Models.Archivo_Versionado>()
                                    {
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"}
                                    }
            });
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        [JFUnathorizedJSONResult()]
        public JsonResult Eliminar(string nombre)
        {
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido eliminar el archivo debido a que no existe o no se ha especificado ningun archivo",
                                                        titulo: "Eliminación de archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El archivo se ha eliminado correctamente.", titulo: "Eliminación de Archivo", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion
    }
}
