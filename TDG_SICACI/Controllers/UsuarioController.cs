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
    public class UsuarioController : BaseController
    {
        //
        // GET: /Usuario/

        [HttpGet()]
        public ActionResult Index()
        {
            //TODO: agregar logica del metodo, este metodo deberia de mostrarte todos los usuarios si sos el admin, si no lo sos entonces te va a mostrar el consultar de tu propia cuenta
            if (User.IsInRole("Administrador")) {
                SICACI_DAL db = new SICACI_DAL();
                var users = db.IUsers.GetUserList("Activo").Select(m => new Models.UsuarioModel
                {
                    usuario = m.USUARIO,
                    nombre = m.NOMBRES,
                    apellido = m.APELLIDOS,
                    email = m.CORREO_ELECTRONICO,
                    rol = m.TIPO_ROL
                }).ToArray();

                return View(users);

            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");

            //List<Models.UsuarioModel> usuarios = new List<Models.UsuarioModel>()
            //{
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //    new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo"},
            //};

            //return View(usuarios);
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

        [HttpGet()]
        [Authorize(Roles="Administrador")]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Consultar(string usuario)
        {
            SICACI_DAL db = new SICACI_DAL();
            var user = db.IUsers.GetInfoUser(usuario);
            return View(new Models.UsuarioModel
            {
                usuario = user.USUARIO,
                nombre = user.NOMBRES,
                apellido = user.APELLIDOS,
                email = user.CORREO_ELECTRONICO,
                rol = user.TIPO_ROL
            });
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
