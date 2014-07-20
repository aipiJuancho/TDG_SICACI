using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;
using JertiFramework.Interpretes;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Controllers
{
    public class ArchivoController : BaseController
    {
        [HttpGet()]
        public ActionResult Index()
        {
            List<Models.ArchivoModel> archivos = new List<Models.ArchivoModel>()
            {
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" },
                new Models.ArchivoModel { id = 1, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" }

            };

            return View(archivos);        
        }

       [HttpGet]
        public ActionResult Agregar()
        {
            //TODO: agregar logica del metodo
            return View();
        }

       [HttpPost]
       [JFValidarModel()]
       public JsonResult Agregar(Models.ArchivoModel model)
       {
           //TODO: agregar logica del metodo
           return Json(new
           {
               success = true,
               notify = new JFNotifySystemMessage("El archivo se ha agregado correctamente.", titulo: "Completado", permanente: true, icono: JFNotifySystemIcon.Send),
               redirectURL = "/Archivo/"
           });
       }

       [HttpGet()]
       [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Consultar(int id)
        {
            Models.ArchivoModel model = new Models.ArchivoModel { id = id, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" };
            return View(model);
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Modificar(int id)
        {
            Models.ArchivoModel model = new Models.ArchivoModel { id = id, etiqueta = "Etiqueta del archivo", nombre = "Nombre del archivo", url = "/Archivos/archivo.pdf" };
            return View(model);
        }

        [HttpPost]
        [JFValidarModel()]
        public JsonResult Modificar(Models.ArchivoModel model)
        {
            //TODO: agregar logica del metodo
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El archivo se ha modificado correctamente.", titulo: "Completado", permanente: true, icono: JFNotifySystemIcon.Send),
                redirectURL = "/Archivo/"
            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Eliminar(int id)
        {
            //TODO: agregar logica del metodo
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El archivo se ha eliminado correctamente.", titulo: "Completado", permanente: true, icono: JFNotifySystemIcon.Send),
                redirectURL = "/Archivo/"
            });
        }
    }
}
