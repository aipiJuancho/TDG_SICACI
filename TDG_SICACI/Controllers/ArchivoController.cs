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
           //FIXME: no me deja subir el archivo me sale un error y este error si sale bien con ek jf, pero la cosa es que trato de subir un pdf y no me deja =S
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
            //FIXME: da un error el campo archivo no es requerido pero sigue dando este error "{"success":false,"modelErrors":[{"ID_Object":"documento","MSG_Error":"El campo Archivo no es válido."}]}"
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
            //TODO: agregar logica del metodo'
            //FIXME: da un error "{"msg":"Esta solicitud se ha bloqueado porque no se puede revelar información confidencial en sitios web de terceros cuando se utiliza en una solicitud GET. Para permitir solicitudes GET, establezca JsonRequestBehavior en AllowGet."}"
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El archivo se ha eliminado correctamente.", titulo: "Completado", permanente: true, icono: JFNotifySystemIcon.Send),
                redirectURL = "/Archivo/"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
