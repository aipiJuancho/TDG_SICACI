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

        [HttpPost()]
        [JFValidarModel()]
        [JFHandleExceptionMessage(Order=1)]
        public JsonResult GuardarPregunta(Models.Ej1ViewModel model)
        {
            
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }


    }
}
