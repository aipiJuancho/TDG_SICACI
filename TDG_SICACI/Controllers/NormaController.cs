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
    public class NormaController : Controller
    {
        //
        // GET: /Norma/

        public ActionResult Index()
        {
            //TODO: agregar logica del metodo este deberia servir para consultar toda la norma
            return View();
        }

        public ActionResult Consultar()
        {
            //TODO: agregar logica del metodo este deberia servir para consultar un numeral de la norma
            return View();
        }

        // los demas metodos no se si deberian poder ocuparse para la norma //

        //public ActionResult Agregar()
        //{
        //    //TODO: agregar logica del metodo
        //    return View();
        //}
        //public ActionResult Modificar()
        //{
        //    //TODO: agregar logica del metodo
        //    return View();
        //}

        //public ActionResult Eliminar()
        //{
        //    //TODO: agregar logica del metodo
        //    return View();
        //}
    }
}
