using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDG_SICACI.Controllers
{
    public class preguntaController : Controller
    {
        //
        // GET: /pregunta/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult agregarPregunta()
        {
            return View();
        }
    }
}
