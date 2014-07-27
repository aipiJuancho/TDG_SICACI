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
    public class findingController : BaseController
    {
        //
        // GET: /finding/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult agregarFinding()
        {
            return View();
        }

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
