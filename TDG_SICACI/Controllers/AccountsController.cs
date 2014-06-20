using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;

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

    }
}
