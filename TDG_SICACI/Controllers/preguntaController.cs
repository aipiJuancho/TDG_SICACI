using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;
using JertiFramework.Interpretes;
using JertiFramework.Controls;
using TDG_SICACI.Database.DAL;


namespace TDG_SICACI.Controllers
{
    public class PreguntaController : BaseController
    {
        //
        // GET: /pregunta/

        [HttpGet()]
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        [Authorize(Roles = "Administrador")]
        public JsonResult _get_grid_preguntas(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IPreguntas.GetPreguntaList().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IPreguntas.GetPreguntaList());

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_PreguntasViewModel
                {
                    ID_Jerarquia = u.ID_JERARQUIA.Value,
                    Descripcion_Jerarquia = u.DESCRIPCION_JERARQUIA,
                    Tipo_Pregunta = u.TIPO_PREGUNTA,
                    Asociado_A = u.ASOCIADO_A,
                    Arbol = u.ARBOL
                });

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = db.IPreguntas.GetPreguntaList().Count(),
                page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        [Authorize(Roles = "Administrador")]
        public ActionResult AgregarPregunta()
        {
            return View();
        }

        public ActionResult agregarPreguntaAbierta(string padd)
        {
            return View();
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

            List<SelectListItem> catego = new List<SelectListItem>();
            catego.Add(new SelectListItem() { Text = "Categoria 1", Value = "CA1", Selected = true });
            catego.Add(new SelectListItem() { Text = "Categoria 2", Value = "CA2" });
            catego.Add(new SelectListItem() { Text = "Categoraia 3", Value = "CA3" });
            return Json(new JFComboboxToJSON(catego), JsonRequestBehavior.AllowGet);

            List<SelectListItem> relacionado = new List<SelectListItem>();
            relacionado.Add(new SelectListItem() { Text = "Numeral 1 norma", Value = "NMA", Selected = true });
            relacionado.Add(new SelectListItem() { Text = "Numeral 2 norma", Value = "NME" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 3 norma", Value = "OB" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 4 norma", Value = "OM" });
            return Json(new JFComboboxToJSON(relacionado), JsonRequestBehavior.AllowGet);

            List<SelectListItem> requerido = new List<SelectListItem>();
            requerido.Add(new SelectListItem() { Text = "PDF", Value = "PDF", Selected = true });
            requerido.Add(new SelectListItem() { Text = "DOCX", Value = "DOC" });
            requerido.Add(new SelectListItem() { Text = "JPEG", Value = "JPEG" });
            return Json(new JFComboboxToJSON(requerido), JsonRequestBehavior.AllowGet);

        }

        public ActionResult agregarOpcionMultiple()
        {
            return View();
        }

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarOpcionComboBox(Models.findingModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _get_OpcionComboBox()
        {
            //Genero una pausa de 3 segundos en el codigo para que puedan observar la carga
            System.Threading.Thread.Sleep(3000);

            List<SelectListItem> catego = new List<SelectListItem>();
            catego.Add(new SelectListItem() { Text = "Categoria 1", Value = "CA1", Selected = true });
            catego.Add(new SelectListItem() { Text = "Categoria 2", Value = "CA2" });
            catego.Add(new SelectListItem() { Text = "Categoraia 3", Value = "CA3" });
            return Json(new JFComboboxToJSON(catego), JsonRequestBehavior.AllowGet);

            List<SelectListItem> relacionado = new List<SelectListItem>();
            relacionado.Add(new SelectListItem() { Text = "Numeral 1 norma", Value = "NMA", Selected = true });
            relacionado.Add(new SelectListItem() { Text = "Numeral 2 norma", Value = "NME" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 3 norma", Value = "OB" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 4 norma", Value = "OM" });
            return Json(new JFComboboxToJSON(relacionado), JsonRequestBehavior.AllowGet);

            List<SelectListItem> requerido = new List<SelectListItem>();
            requerido.Add(new SelectListItem() { Text = "PDF", Value = "PDF", Selected = true });
            requerido.Add(new SelectListItem() { Text = "DOCX", Value = "DOC" });
            requerido.Add(new SelectListItem() { Text = "JPEG", Value = "JPEG" });
            return Json(new JFComboboxToJSON(requerido), JsonRequestBehavior.AllowGet);

        }

        public ActionResult agregarSeleccionMultiple()
        {
            return View();
        }

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarSeleccionComboBox(Models.findingModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _get_SeleccionComboBox()
        {
            //Genero una pausa de 3 segundos en el codigo para que puedan observar la carga
            System.Threading.Thread.Sleep(3000);

            List<SelectListItem> catego = new List<SelectListItem>();
            catego.Add(new SelectListItem() { Text = "Categoria 1", Value = "CA1", Selected = true });
            catego.Add(new SelectListItem() { Text = "Categoria 2", Value = "CA2" });
            catego.Add(new SelectListItem() { Text = "Categoraia 3", Value = "CA3" });
            return Json(new JFComboboxToJSON(catego), JsonRequestBehavior.AllowGet);

            List<SelectListItem> relacionado = new List<SelectListItem>();
            relacionado.Add(new SelectListItem() { Text = "Numeral 1 norma", Value = "NMA", Selected = true });
            relacionado.Add(new SelectListItem() { Text = "Numeral 2 norma", Value = "NME" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 3 norma", Value = "OB" });
            relacionado.Add(new SelectListItem() { Text = "Numeral 4 norma", Value = "OM" });
            return Json(new JFComboboxToJSON(relacionado), JsonRequestBehavior.AllowGet);

            List<SelectListItem> requerido = new List<SelectListItem>();
            requerido.Add(new SelectListItem() { Text = "PDF", Value = "PDF", Selected = true });
            requerido.Add(new SelectListItem() { Text = "DOCX", Value = "DOC" });
            requerido.Add(new SelectListItem() { Text = "JPEG", Value = "JPEG" });
            return Json(new JFComboboxToJSON(requerido), JsonRequestBehavior.AllowGet);

        }
    }
}
