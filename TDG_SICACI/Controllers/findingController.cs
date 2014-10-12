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
using JertiFramework.Controls;

namespace TDG_SICACI.Controllers
{
    public class findingController : BaseController
    {
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol   = "Administrador";
        private const string kItemType  = "Archivo";   
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        public ActionResult Index()
        {
            if (User.IsInRole(kUserRol))
            {
                return View();
            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }
        //--------------------------------------------------------------------------------------------------------------//
        [HttpPost()]
        [Authorize(Roles = kUserRol)]
        public JsonResult DataGrid(jfBSGrid_Respond model)
        {
            //la data es dummy, por eso no funciona la paginacion correctamente porque como este metodo se ejecuta con cada 
            //request ajax la data se presenta siempre estatica

            List<Models.Grid_FindingViewModel> items = new List<Models.Grid_FindingViewModel>()
            {
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"},
                new Models.Grid_FindingViewModel { id = 1, comentario = "Comentario del finding", tipoNoConformidad = "No conformidad menor", numeralRelacionado = 4, tipoCorreccion = "Sostenible", fechaLimiteSugerida = DateTime.Today, estado = "Pendiente"}
            };
            return Json(new jfBSGrid_ReturnData
            {
                total_rows = items.Count(),
                page_data = items
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Create
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = kUserRol)]
        public ActionResult Agregar()
        {
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_ArchivoModel model)//TODO: comprobar el Modelo
        {
            SICACI_DAL db = new SICACI_DAL();
            // db.IUsers.CrearUsuario(model.Usuario, model.Nombres, model.Apellidos, model.CorreoE, model.Password, model.Rol);
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El " + kItemType + " se ha creado correctamente", titulo: "Nuevo " + kItemType, permanente: true, icono: JFNotifySystemIcon.NewDoc)
            });
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Read

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Consultar(int id)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el finding que se desea consultar.",
                                                        titulo: "Consultar un Finding",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return View(new Models.Consultar_ArchivoModel
            {
                nombre = "nombre del archivo",
                etiqueta = "etiqueta del archivo",
                archivosVersionados = new List<Models.Archivo_Versionado>()
                                    {
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"}
                                    }

            });
        }
        #endregion




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar(string nombre)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (string.IsNullOrWhiteSpace(nombre))
            {
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




            return PartialView(new Models.Modificar_ArchivoModel
            {
                nombre = "nombre del archivo",
                etiqueta = "etiqueta del archivo",
                archivosVersionados = new List<Models.Archivo_Versionado>()
                                    {
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"},
                                         new Models.Archivo_Versionado { fecha =  DateTime.Today, url = "#"}
                                    }
            });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public JsonResult Eliminar(string nombre)
        {
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido eliminar el archivo debido a que no existe o no se ha especificado ningun archivo",
                                                        titulo: "Eliminación de archivo",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El archivo se ha eliminado correctamente.", titulo: "Eliminación de Archivo", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion












        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarRadioCombo(Models.findingModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarComboBox(Models.findingModel model)
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

            List<SelectListItem> NoConformidad = new List<SelectListItem>();
            NoConformidad.Add(new SelectListItem() { Text = "No conformidad mayor", Value = "NMA", Selected = true });
            NoConformidad.Add(new SelectListItem() { Text = "No conformidad menor", Value = "NME" });
            NoConformidad.Add(new SelectListItem() { Text = "Observacion", Value = "OB" });
            NoConformidad.Add(new SelectListItem() { Text = "Oportunidad de mejora", Value = "OM" });
            return Json(new JFComboboxToJSON(NoConformidad), JsonRequestBehavior.AllowGet);

            List<SelectListItem> relacionado = new List<SelectListItem>();
            relacionado.Add(new SelectListItem() { Text = "Numeral 1 norma", Value = "NMA", Selected = true });
            relacionado.Add(new SelectListItem() { Text = "Numeral 2 norma", Value = "NME" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 3 norma", Value = "OB" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 4 norma", Value = "OM" });
            return Json(new JFComboboxToJSON(relacionado), JsonRequestBehavior.AllowGet);
        }

       

    }
}
