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
using System.Globalization;

namespace TDG_SICACI.Controllers
{
    public class findingController : BaseController
    {
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol   = "Administrador";
        private const string kItemType  = "Finding";   
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
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IFindings.GetAll().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IFindings.GetAll());

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(f => new Models.Grid_FindingViewModel
                {
                    ID = f.ID,
                    COMENTARIO = f.COMENTARIO,
                    TIPO_NOCONFORMIDAD = f.TIPO_NOCONFORMIDAD,
                    TIPO_CORRECION = f.TIPO_CORRECION,
                    FECHA_LIMITE = (f.FECHA_LIMITE.HasValue ? f.FECHA_LIMITE.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty),
                    ESTADO = f.ESTADO
                });

            var filas = db.IFindings.GetAll().Count();

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = filas,
                page_data = dataUsers
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
            //Definimos el ComboBox de Tipo de No Conformidad
            var NoConformidad = new List<SelectListItem>();
            NoConformidad.Add(new SelectListItem() { Text = "No conformidad mayor", Value = "1", Selected = true });
            NoConformidad.Add(new SelectListItem() { Text = "No conformidad menor", Value = "2" });
            NoConformidad.Add(new SelectListItem() { Text = "Observacion", Value = "3" });
            NoConformidad.Add(new SelectListItem() { Text = "Oportunidad de mejora", Value = "4" });

            //Definimos el ComboBox de Tipo de Correción
            var correccion = new List<SelectListItem>();
            correccion.Add(new SelectListItem() { Text = "Inmediata", Value = "IN", Selected = true });
            correccion.Add(new SelectListItem() { Text = "Sostenible", Value = "SO" });

            ViewBag.TipoNoConformidad = NoConformidad;
            ViewBag.TIpoCorreccion = correccion;
            return PartialView();
        }

        [HttpPost]
        [JFValidarModel()]
        [Authorize(Roles = kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult Agregar(Models.Agregar_FindingModel model)//TODO: comprobar el Modelo
        {
            SICACI_DAL db = new SICACI_DAL();
            db.IFindings.Create_Finding(model.tipoNoConformidad, model.comentario, model.tipoCorreccion,
                model.accionCorrectivaSugerida, model.fechaLimiteSugerida, User.Identity.Name);
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
            var db = new SICACI_DAL();

            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0 || db.IFindings.GetAll().Where(f => f.ID.Equals(id)).Count().Equals(0))
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

            var item = db.IFindings.GetAll().Where(f => f.ID.Equals(id)).SingleOrDefault();
            return View(new Models.Consultar_FindingModel
            {
                ID = item.ID,
                COMENTARIO = item.COMENTARIO,
                TIPO_NOCONFORMIDAD = item.TIPO_NOCONFORMIDAD,
                TIPO_CORRECION = item.TIPO_CORRECION,
                ACCION_CORRECTIVA_SUGERIDA = item.ACCION_CORRECTIVA_SUGERIDA,
                ESTADO = item.ESTADO,
                USUARIO = item.USUARIO,
                FECHA_LIMITE = (item.FECHA_LIMITE.HasValue ? item.FECHA_LIMITE.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty),
                FECHA_CREACION = item.FECHA_CREACION.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
            });
        }
        #endregion




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar(int id)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha especificado en la solicitud el finding que se desea modificar.",
                                                        titulo: "Modificación de finding",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(new Models.Modificar_FindingModel
            {
                id = 1, 
                tipoNoConformidad = "No conformidad menor",
                comentario = "comentario del finding",
                numeralRelacion = 4,
                tipoCorreccion = "Inmediata",
                accionCorrectivaSugerida = "texto de la accion correctiva sugerida",
                fechaLimiteSugerida = DateTime.Today
            });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delete
        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public JsonResult Eliminar(int id)
        {
            //Antes de seguir, validamos que se haya pasado un nombre de usuario en el sistema
            if (id == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("No se ha podido eliminar el finding debido a que no existe o no se ha especificado ningun Finding",
                                                        titulo: "Eliminación de finding",
                                                        permanente: false,
                                                        tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El finding se ha eliminado correctamente.", titulo: "Eliminación de finding", permanente: true, icono: JFNotifySystemIcon.Delete)
            });
        }
        #endregion












        //[HttpPost()]
        //[JFValidarModel()]
        //public JsonResult GuardarRadioCombo(Models.findingModel model)
        //{
        //    return Json(new
        //    {
        //        success = true,
        //        notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
        //    });
        //}

        //[HttpPost()]
        //[JFValidarModel()]
        //public JsonResult GuardarComboBox(Models.findingModel model)
        //{
        //    return Json(new
        //    {
        //        success = true,
        //        notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
        //    });
        //}

        //[HttpPost()]
        //[JFHandleExceptionMessage(Order = 1)]
        //public JsonResult _get_ComboBox()
        //{
        //    //Genero una pausa de 3 segundos en el codigo para que puedan observar la carga
        //    System.Threading.Thread.Sleep(3000);

        //    List<SelectListItem> NoConformidad = new List<SelectListItem>();
        //    NoConformidad.Add(new SelectListItem() { Text = "No conformidad mayor", Value = "NMA", Selected = true });
        //    NoConformidad.Add(new SelectListItem() { Text = "No conformidad menor", Value = "NME" });
        //    NoConformidad.Add(new SelectListItem() { Text = "Observacion", Value = "OB" });
        //    NoConformidad.Add(new SelectListItem() { Text = "Oportunidad de mejora", Value = "OM" });
        //    return Json(new JFComboboxToJSON(NoConformidad), JsonRequestBehavior.AllowGet);

        //    List<SelectListItem> relacionado = new List<SelectListItem>();
        //    relacionado.Add(new SelectListItem() { Text = "Numeral 1 norma", Value = "NMA", Selected = true });
        //    relacionado.Add(new SelectListItem() { Text = "Numeral 2 norma", Value = "NME" });
        //    relacionado.Add(new SelectListItem() { Text = "Numeral 3 norma", Value = "OB" });
        //    relacionado.Add(new SelectListItem() { Text = "Numeral 4 norma", Value = "OM" });
        //    return Json(new JFComboboxToJSON(relacionado), JsonRequestBehavior.AllowGet);
        //}

       

    }
}
