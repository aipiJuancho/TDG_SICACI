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
    public class PreguntaSeleccionMultipleController : BaseController
    {
        //
        // GET: /PreguntaSeleccionMultiple/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult agregar()
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

        public ActionResult consultar()
        {
            return View();
        }

        public ActionResult modificar()
        {
            return View();
        }

        public ActionResult eliminar() //ocultar???
        {
            return View();
        }
    }
}
