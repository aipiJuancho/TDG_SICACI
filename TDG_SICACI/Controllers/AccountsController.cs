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

namespace TDG_SICACI.Controllers
{
    public class AccountsController : Controller
    {

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
            //throw new ApplicationException("Esto es una prueba");

            FormsAuthentication.SetAuthCookie(model.UserName, false);
            return Json(new
            {
                success = true,
                redirectURL = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
            });

            //Response.TrySkipIisCustomErrors = true;
            //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //return Json(new { notify = new JFNotifySystemMessage("El nombre de usuario o contraseña ingresadas son incorrectas", titulo: "Inicio de Sesión Fallido", permanente: false, tiempo: 5000) }, JsonRequestBehavior.AllowGet);
        }

    }
}
