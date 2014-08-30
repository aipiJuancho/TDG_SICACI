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
    public class OrganizacionController : BaseController
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
                return View(new Models.Consultar_OrganizacionModel
                {
                    nombre  =   "Academia Criatiana Internacional",
                    logo    =   "http://i.imgur.com/txDeCph.gif",
                    eslogan =   "eslogan de la compania", 
                    alcance =   "Texto del alcance Texto del alcance Texto del alcance Texto del alcance Texto del alcance",
                    mision  =   "mision de la compania mision de la compania mision de la compania mision de la compania mision de la compania",
                    vision  =   "vision de la pompania vision de la pompania vision de la pompania vision de la pompania vision de la pompania"
                    
                });
            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }
        #endregion
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar()
        {

            return View(new Models.Modificar_organizacionModel
            {
                nombre = "Academia Criatiana Internacional",
                eslogan = "eslogan de la compania",
                alcance = "Texto del alcance Texto del alcance Texto del alcance Texto del alcance Texto del alcance",
                mision = "mision de la compania mision de la compania mision de la compania mision de la compania mision de la compania",
                vision = "vision de la pompania vision de la pompania vision de la pompania vision de la pompania vision de la pompania"

            });
        }
        #endregion

    }
}
