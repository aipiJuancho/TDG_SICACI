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
    public class EjemploController : Controller
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

            Models.EjemploViewModel demo = new Models.EjemploViewModel() { Pregunta = "ola k ase", Respuesta = "nada" };

            return View(demo);
        }

        [HttpPost()]
        [JFValidarModel()]
        //[JFHandleExceptionMessage(Order=1)]
        public JsonResult GuardarPregunta(Models.EjemploViewModel model)
        {
            //Para guardar los archivos
            //http://stackoverflow.com/questions/10856240/asp-mvc-file-upload-httppostedfilebase-is-null
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        public ActionResult NuevosBotones()
        {
            return View();
        }

    }
}
