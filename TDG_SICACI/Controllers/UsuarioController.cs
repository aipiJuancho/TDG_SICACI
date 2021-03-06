﻿using System;
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
using JertiFramework.Controls;

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
                return View();
            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }

        
        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = "Administrador")]
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
        [JFAutorizationSecurity(Roles = "Administrador")]
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

        [HttpPost()]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        public JsonResult _get_grid_users(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IUsers.GetUserList().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IUsers.GetUserList());

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_UserViewModel()
                {
                    nombres = u.NOMBRES,
                    apellidos = u.APELLIDOS,
                    usuario = u.USUARIO,
                    email = u.CORREO_ELECTRONICO,
                    estado = u.ACTIVO,
                    rol = u.TIPO_ROL
                });

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = db.IUsers.GetUserList().Count(),
                page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
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

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        public ActionResult _get_crear_user()
        {
            SICACI_DAL db = new SICACI_DAL();

            //Generamos el ComboBox del Tipo de Usuario
            var u = db.IUsers.GetRoles().Select(r => new SelectListItem()
            {
                Text = r.TIPO_ROL,
                Value = r.ID_ROL.ToString()
            }).ToArray();
            ViewBag.Roles = u;

            return PartialView();
        }

        [HttpGet()]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        public JsonResult _validateUser(string Usuario)
        {
            SICACI_DAL db = new SICACI_DAL();
            if (db.IUsers.IsExistUser(Usuario))
            {
                return Json("Ya existe un usuario en el sistema con este nombre de usuario.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [JFValidarModel()]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult CrearUsuario(Models.NewUserViewModel model)
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IUsers.CrearUsuario(model.Usuario, model.Nombres, model.Apellidos, model.CorreoE, model.Password, model.Rol);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El usuario se ha creado correctamente", titulo: "Nuevo usuario", permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpGet()]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost()]
        [JFValidarModel()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult ChangePassword(Models.ChangePasswordViewModel model)
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IUsers.CambiarContraseña(User.Identity.Name, model.oldPassword, model.newPassword, model.confirmNewPassword);
            return Json(new
            {
                success = true,
                redirectURL = Url.Action("Index", "Home")
            });
        }

        [HttpGet()]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult ChangePWDUser(string usuario = "")
        {
            if (string.IsNullOrEmpty(usuario))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se puede continuar debido a que no ha seleccionado ningun usuario.",
                                                        titulo: "No hay usuario seleccionado",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            SICACI_DAL db = new SICACI_DAL();
            if (db.IUsers.GetUserList().Where(u => u.USUARIO.ToUpper().Equals(usuario.ToUpper())).Count() == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("Lo sentimos, pero el usuario seleccionado no existe en la base de datos.",
                                                        titulo: "No existe el usuario seleccionado",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [HttpPost()]
        [JFValidarModel()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _ChangePWDUser(Models.ChangePasswordUserViewModel model, string usuario)
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IUsers.ChangePWD_User(usuario, model.newPassword, User.Identity.Name);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage(string.Format("Se ha modificado la contraseña del usuario {0}", usuario), titulo: "Cambio de contraseña", permanente: false, icono: JFNotifySystemIcon.Update)
            });
        }
    }
}
