using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using TDG_SICACI.Models;
using System.Web.Security;
using JertiFramework.Interpretes.NotifySystem;
using System.Net;
using TDG_SICACI.Providers;

namespace TDG_SICACI.Controllers
{
    public class AccountsController : Controller
    {

        #region "Inicialización de Proveedores"
            public TDGMembershipProvider MP_SICACI {get; set;}
            public TDGRoleProvider RP_SICACI { get; set; }

           protected override void Initialize(System.Web.Routing.RequestContext requestContext)
            {
                if (MP_SICACI == null)
                    MP_SICACI = new TDGMembershipProvider();

                if (RP_SICACI == null)
                    RP_SICACI = new TDGRoleProvider();

                base.Initialize(requestContext);
            }
        #endregion

        [HttpGet()]
        [JFAllowAnonymous()]
        public ActionResult LogOn(string returnUrl)
        {
            return View();
        }

        [HttpPost()]
        [JFAllowAnonymous()]
        [JFValidarModel()]
        [JFHandleExceptionMessage(Order=1)]
        public JsonResult LogOn(LoginViewModel model, string returnUrl)
        {
            var db = new TDG_SICACI.Database.DAL.SICACI_DAL();
            //Comprobamos que el USER y PWD sean validos en el sistema
            if (MP_SICACI.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                
                db.IUsers.RegistrarEventoBitacora("Inicio de Sesión", model.UserName, "El usuario ha iniciado sesión dentro de la aplicación", string.Empty, string.Empty);
                return Json(new
                {
                    success = true,
                    redirectURL = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
                });
            }
            else
            {
                db.IUsers.RegistrarEventoBitacora("Inicio de Sesión", model.UserName, "El usuario ha fallado en su contraseña al intentar inciar sesión", string.Empty, model.Password);
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("El nombre de usuario o contraseña ingresadas son incorrectas",
                                                        titulo: "Inicio de Sesión Fallido",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet()]
        public ActionResult SignOut()
        {
            var db = new TDG_SICACI.Database.DAL.SICACI_DAL();
            db.IUsers.RegistrarEventoBitacora("Inicio de Sesión", User.Identity.Name, "El usuario ha cerrado su sesión manualmente", string.Empty, string.Empty);
            FormsAuthentication.SignOut();
            return RedirectToRoute("Default", new { controller = "Accounts", action = "LogOn" });
        }
    }
}
