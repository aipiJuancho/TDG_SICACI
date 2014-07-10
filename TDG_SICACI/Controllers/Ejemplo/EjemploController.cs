﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;
using JertiFramework.Interpretes;

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

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarRadioCombo(Models.RadioComboViewModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarComboBox(Models.RadioComboViewModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _get_ComboBox()
        {
            //Genero una pausa de 3 segundos en el codigo para que puedan observar la carga
            System.Threading.Thread.Sleep(3000);

            List<SelectListItem> dpto = new List<SelectListItem>();
            dpto.Add(new SelectListItem() { Text = "San Salvador", Value = "SS", Selected = true });
            dpto.Add(new SelectListItem() { Text = "Ahuachapan", Value = "AH" });
            dpto.Add(new SelectListItem() { Text = "Santa Ana", Value = "SA" });
            dpto.Add(new SelectListItem() { Text = "San Miguel", Value = "SM" });
            dpto.Add(new SelectListItem() { Text = "San Vicente", Value = "SV" });
            dpto.Add(new SelectListItem() { Text = "La Union", Value = "LU" });

            return Json(new JFComboboxToJSON(dpto), JsonRequestBehavior.AllowGet);
        }


    }
}
