using System;
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
    public class UsuarioController : BaseController
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            //TODO: agregar logica del metodo, este metodo deberia de mostrarte todos los usuarios si sos el admin, si no lo sos entonces te va a mostrar el consultar de tu propia cuenta
            List<Models.UsuarioModel> usuarios = new List<Models.UsuarioModel>()
            {
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
                new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0},
            };

            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            //TODO: agregar logica del metodo
            return View();
        }

        [HttpPost]
        [JFValidarModel()]
        public JsonResult Agregar(Models.UsuarioModel model)
        {
            //TODO: agregar logica del metodo
            return Json(new
            {
                success = true,
                redirectURL = "/Usuario/"
            });
        }

        public ActionResult Consultar(string usuario)
        {
            //TODO: agregar logica del metodo
            Models.UsuarioModel model = new Models.UsuarioModel { usuario = usuario, nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = 0 };
            return View(model);
        }

        public ActionResult Modificar()
        {
            //TODO: agregar logica del metodo
            return View();
        }

        public ActionResult Eliminar()
        {
            //TODO: agregar logica del metodo
            return View();
        }

    }
}
