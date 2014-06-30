using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using TDG_SICACI.Models;

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
        public ActionResult LogOn(LoginViewModel model, string returnUrl)
        {
            return View();
        }

    }
}
