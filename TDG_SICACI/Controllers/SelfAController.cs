using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDG_SICACI.Controllers
{
    public class SelfAController : Controller
    {
        //
        // GET: /SelfA/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreaPregunta(string id)
        {
            List<string> lst = new List<string>();
            lst.Add("Juan");
            lst.Add("Nelson");
            lst.Add("Sofy");

            
            ViewBag.Name = id;
            ViewBag.Nombres = lst;

            Models.Ej1ViewModel demo = new Models.Ej1ViewModel() { Pregunta = "ola k ase", Respuesta = "nada" };

            return View(demo);
        }


    }
}
