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
using System.Net;

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
                var users = db.IUsers.GetUserList().Select(m => new Models.UsuarioModel
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
                rol = user.TIPO_ROL,
                estado = user.ACTIVO
            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Modificar(string usuario)
        {
            //TODO: agregar logica del metodo
            Models.UsuarioModel model = new Models.UsuarioModel { usuario = "jc.garcia", nombre = "Juan Carlos", apellido = "Garcia Alfaro", email = "jc.garcia@jerti.com", rol = "Demo" };
            return View(model);
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = "Administrador")]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Modificar(Models.UsuarioModifiyModel model, string usuario)
        {
            //Validamos que se nos haya transferido el usuario a ser modificado
            if (string.IsNullOrWhiteSpace(usuario))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado el usuario al cual se desea realizar dicha modificación",
                                                        titulo: "Modificación de Datos de Usuario",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            db.IUsers.ModificarUsuario(usuario, model.nombre, model.apellido, model.email, model.rol, model.estado);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El usuario se ha modificado correctamente.", titulo: "Completado", permanente: true, icono: JFNotifySystemIcon.Update)
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public JsonResult Eliminar(string usuario)
        {
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            if (string.IsNullOrWhiteSpace(usuario)) {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido eliminar el usuario debido a que no existe o no se ha especificado ningun usuario",
                                                        titulo: "Eliminación de usuario",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            db.IUsers.EliminarUsuario(usuario);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El usuario se ha eliminado correctamente.", titulo: "Eliminación de Usuario", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult _get_table_user() {
            SICACI_DAL db = new SICACI_DAL();
            var users = db.IUsers.GetUserList().Select(m => new Models.UsuarioModel
            {
                usuario = m.USUARIO,
                nombre = m.NOMBRES,
                apellido = m.APELLIDOS,
                email = m.CORREO_ELECTRONICO,
                rol = m.TIPO_ROL,
                estado = m.ACTIVO
            }).ToArray();
            return PartialView(users);
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult _get_modificar_user(string usuario)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (string.IsNullOrWhiteSpace(usuario)) {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el usuario que se desea modificar.",
                                                        titulo: "Modificación de Usuario",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            //Si esta correcto, recuperamos la información del usuario especificado
            SICACI_DAL db = new SICACI_DAL();
            var dataUser = db.IUsers.GetInfoUser(usuario);

            //Generamos el ComboBox del Tipo de Usuario
            var u = db.IUsers.GetRoles().Select(r => new SelectListItem()
            {
                Text = r.TIPO_ROL,
                Value = r.ID_ROL.ToString(),
                Selected = (r.TIPO_ROL == dataUser.TIPO_ROL ? true : false)
            }).ToArray();
            ViewBag.Roles = u;

            //Generamos el ComboBox de Estado
            List<SelectListItem> status = new List<SelectListItem>() {
                new SelectListItem() {Text = "Activo", Value = "Activo", Selected = (dataUser.ACTIVO.Equals("Activo") ? true : false)},
                new SelectListItem() {Text = "Inactivo", Value = "Inactivo", Selected = (dataUser.ACTIVO.Equals("Inactivo") ? true : false)}
            };
            ViewBag.Status = status;

            return PartialView(new Models.UsuarioModifiyModel {
                nombre = dataUser.NOMBRES,
                apellido = dataUser.APELLIDOS,
                email = dataUser.CORREO_ELECTRONICO,
                estado = dataUser.ACTIVO
            });
        }

    }
}
