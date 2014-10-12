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

namespace TDG_SICACI.Controllers
{
    public class ArchivoController : BaseController
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol   = "Administrador";
        private const string kItemType  = "Archivo";   
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

            List<Models.Grid_ArchivoViewModel> items = new List<Models.Grid_ArchivoViewModel>()
            {
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre Archvivo", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"}
            };
            return Json(new jfBSGrid_ReturnData
            {
                total_rows = items.Count(),
                page_data = items
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
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
        [Authorize(Roles = kUserRol)]
        public ActionResult _newModal_FileGroup()
        {
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _crear_filegroup_name(Models.New_FileGroupName model)//TODO: comprobar el Modelo
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Consultar(string nombre)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el archvivo que se desea consultar.",
                                                        titulo: "Consultar un Archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return View(new Models.Consultar_ArchivoModel
            {
                nombre                  = "nombre del archivo",
                etiqueta                = "etiqueta del archivo",
                archivosVersionados     = new List<Models.Archivo_Versionado>()
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
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
