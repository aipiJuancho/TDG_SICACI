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


namespace TDG_SICACI.Controllers
{
    public class NormaController : BaseController
    {
        //
        // GET: /Norma/
        [HttpGet()]
        public ActionResult Index()
        {
            //TODO: agregar logica del metodo este deberia servir para consultar toda la norma
            List<Models.NormaModel> norma = new List<Models.NormaModel>()
            {
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},
                new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma"},

            };

            return View(norma);   
        }

        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Consultar(int numeral)
        {
            //TODO: agregar logica del metodo este deberia servir para consultar un numeral de la norma
            Models.NormaModel model = new Models.NormaModel { numeral = 1, contenido = "texto contenido correspondiente al numeral de la norma" };
            return View(model);
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
