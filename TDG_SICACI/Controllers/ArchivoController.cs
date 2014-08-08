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
        private const string kUserRol = "Administrador";
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
                new Models.Grid_ArchivoViewModel { nombre = "nombre", etiqueta = "etiqueta"},
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
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

       
    }
}
