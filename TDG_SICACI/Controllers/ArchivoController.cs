using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;

namespace TDG_SICACI.Controllers
{
    public class ArchivoController : Controller
    {
        //
        // GET: /Documento/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult Modificar()
        {
            return View();
        }
    }
}
