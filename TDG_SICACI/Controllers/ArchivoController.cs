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
                notify = new JFNotifySystemMessage("El "+kItemType+" se ha creado correctamente", titulo: "Nuevo "+kItemType, permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Read
        
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Consultar(string nombre)
        {
            Console.WriteLine(nombre);
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

            return PartialView(new Models.Consultar_ArchivoModel
            {
                nombre      = "nombre del archivo",
                etiqueta    = "etiqueta del archivo",
                url         = "url del archivo"
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
                nombre = "nombre del archivo",
                etiqueta = "etiqueta del archivo"
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
