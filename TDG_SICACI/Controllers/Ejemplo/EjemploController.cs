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
using JertiFramework.Controls;
using TDG_SICACI.Models;
using TDG_SICACI.Database;

namespace TDG_SICACI.Controllers
{
    public class EjemploController : BaseController
    {
        //
        // GET: /SelfA/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreaPregunta(string id)
        {
            List<string> lst = new List<string>();
            lst.Add("Juan");
            lst.Add("Nelson");
            lst.Add("Sofy");

            
            ViewBag.Name = id;
            ViewBag.Nombres = lst;

            Models.EjemploViewModel demo = new Models.EjemploViewModel() { Pregunta = "ola k ase", Respuesta = "nada" };

            return View(demo);
        }

        [HttpPost()]
        [JFValidarModel()]
        //[JFHandleExceptionMessage(Order=1)]
        public JsonResult GuardarPregunta(Models.EjemploViewModel model)
        {
            //Para guardar los archivos
            //http://stackoverflow.com/questions/10856240/asp-mvc-file-upload-httppostedfilebase-is-null
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        public ActionResult NuevosBotones()
        {
            return View();
        }

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarRadioCombo(Models.RadioComboViewModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("Todos los datos estan correctos", "Titulo del Mensaje", icono: JFNotifySystemIcon.NewDoc)
            });
        }

        [HttpPost()]
        [JFValidarModel()]
        public JsonResult GuardarComboBox(Models.RadioComboViewModel model)
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

            List<SelectListItem> dpto = new List<SelectListItem>();
            dpto.Add(new SelectListItem() { Text = "San Salvador", Value = "SS", Selected = true });
            dpto.Add(new SelectListItem() { Text = "Ahuachapan", Value = "AH" });
            dpto.Add(new SelectListItem() { Text = "Santa Ana", Value = "SA" });
            dpto.Add(new SelectListItem() { Text = "San Miguel", Value = "SM" });
            dpto.Add(new SelectListItem() { Text = "San Vicente", Value = "SV" });
            dpto.Add(new SelectListItem() { Text = "La Union", Value = "LU" });

            return Json(new JFComboboxToJSON(dpto), JsonRequestBehavior.AllowGet);
        }

        private readonly List<Models.ClientViewModel> clients = new List<Models.ClientViewModel>()
        {
            new Models.ClientViewModel { Id = 1, Name = "Julio Avellaneda", Email = "julito_gtu@hotmail.com" },
            new Models.ClientViewModel { Id = 2, Name = "Juan Torres", Email = "jtorres@hotmail.com" },
            new Models.ClientViewModel { Id = 3, Name = "Oscar Camacho", Email = "oscar@hotmail.com" },
            new Models.ClientViewModel { Id = 4, Name = "Gina Urrego", Email = "ginna@hotmail.com" },
            new Models.ClientViewModel { Id = 5, Name = "Nathalia Ramirez", Email = "natha@hotmail.com" },
            new Models.ClientViewModel { Id = 6, Name = "Raul Rodriguez", Email = "rodriguez.raul@hotmail.com" },
            new Models.ClientViewModel { Id = 7, Name = "Johana Espitia", Email = "johana_espitia@hotmail.com" }
        };

        public ActionResult Grid()
        {
            return View(clients);
        }

        [HttpGet]
        public ActionResult Dialog()
        {
            return View();
        }

        [HttpGet]
        [JFHandleExceptionMessage(Order=1)]
        public ActionResult _get_FormDialog()
        {
            return PartialView();
        }

        [HttpGet]
        [JFValidarModel()]
        [JFHandleExceptionMessage(Order = 1)]
        public JsonResult _save_formDialog(Models.ClientViewModel model)
        {
            return Json(new
            {
                success = true,
                notify = new JFNotifySystemMessage("El profesor se ha creado correctamente en el sistema. Además, se le ha enviado al correo electrónico una notificación con su nombre de usuario y contraseña para que pueda acceder al sistema.", titulo: "¡Se creó correctamente!", permanente: true, icono: JFNotifySystemIcon.Send)
            });
        }

        public ActionResult bsGrid()
        {
            return View();
        }

        [HttpPost()]
        public JsonResult _get_datagrid(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ? 
                db.IUsers.GetUserList().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IUsers.GetUserList());

            var dataUsers = data
                .Skip((model.page_num -1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.jfBSGrid_User_ViewModel()
            {
                nombres = u.NOMBRES,
                apellidos = u.APELLIDOS,
                usuario = u.USUARIO
            });
            
            return Json(new jfBSGrid_ReturnData {
              total_rows = db.IUsers.GetUserList().Count(),
              page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
