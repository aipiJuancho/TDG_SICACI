﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDG_SICACI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Organizacion");
        }

        public ActionResult NoAutorizado()
        {
            return View("NoAutorizado");
        }


    }
}
