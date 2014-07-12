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
            //Comprobamos que el USER y PWD sean validos en el sistema
            if (MP_SICACI.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Json(new
                {
                    success = true,
                    redirectURL = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
                });
            }
            else
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("El nombre de usuario o contraseña ingresadas son incorrectas",
                                                        titulo: "Inicio de Sesión Fallido",
                                                        permanente: false,
                                                        tiempo: 5000)}, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
