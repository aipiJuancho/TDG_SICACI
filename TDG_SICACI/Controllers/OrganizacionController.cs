using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Security;
using JertiFramework.Controladores;
using JertiFramework.Interpretes.NotifySystem;

namespace TDG_SICACI.Controllers
{
    public class OrganizacionController : Controller
    {
        //
        // GET: /Organizacion/

        public ActionResult Index()
        {
            //TODO: este metodo debera de redigir el flujo al metodo consultar de esta misma clase
            return View();
        }

        // no debe de permitirse que se agregue mas organizaciones porque solo debe de haber una
        //public ActionResult Agregar()
        //{
        //    //TODO: agregar logica del metodo
        //    return View();
        //}

        public ActionResult Consultar()
        {
            //TODO: agregar logica del metodo
            return View();
        }

        public ActionResult Modificar()
        {
            //TODO: agregar logica del metodo
            return View();
        }

        //La organizacion no va a poder eliminarse porque solo existe una
        //public ActionResult Eliminar()
        //{
        //    //TODO: agregar logica del metodo
        //    return View();
        //}

    }
}
