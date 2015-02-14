using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JertiFramework.Controls;
using JertiFramework.Security;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Organizacion");
        }

        public ActionResult NoAutorizado()
        {
            return View("NoAutorizado");
        }

        [JFAutorizationSecurity(Roles = "Administrador")]
        public ActionResult Log()
        {
            return View();
        }

        [HttpPost()]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        public JsonResult DataGrid(jfBSGrid_Respond model)
        {
            var db = new SICACI_DAL();

            //Antes que nada, verificamos si existe algun parametro de ordenamiento
            var data = (model.sorting != null ?
                db.IUsers.Log().AsQueryable().JFBSGrid_Sort(model.sorting.FirstOrDefault()) :
                db.IUsers.Log());

            //Preparamos la data que regresaremos al Grid
            var dataUsers = data
                .Skip((model.page_num - 1) * model.rows_per_page)
                .Take(model.rows_per_page)
                .Select(u => new Models.Grid_LogViewModel
                {
                    ID = u.ID,
                    TIPO = u.TIPO,
                    DESCRIPCION = u.DESCRIPCION,
                    FECHA = u.FECHA.Value.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US")),
                    USUARIO = u.USUARIO,
                    VALOR_ANTERIOR = u.VALOR_ANTERIOR,
                    VALOR_POSTERIOR = u.VALOR_POSTERIOR
                });

            return Json(new jfBSGrid_ReturnData
            {
                total_rows = db.IUsers.Log().Count(),
                page_data = dataUsers
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        [JFAutorizationSecurity(Roles = "Administrador")]
        [JFUnathorizedJSONResult()]
        public JsonResult _export_csv()
        {
            var db = new SICACI_DAL();
            var data = db.IUsers.Log().ToArray();
            var newList = new List<object>();

            foreach (var item in data)
            {
                var newSubList = new List<string>();
                newSubList.Add(item.ID.ToString());
                newSubList.Add(item.TIPO);
                newSubList.Add(item.DESCRIPCION);
                newSubList.Add(item.FECHA.Value.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US")));
                newSubList.Add(item.USUARIO);
                newSubList.Add(item.VALOR_ANTERIOR);
                newSubList.Add(item.VALOR_POSTERIOR);
                newList.Add(newSubList);
            }


            //var data = db.IUsers.Log().Select(l => new
            //{
                
            //    ID = l.ID,
            //    Tipo = l.TIPO,
            //    Descripcion = l.DESCRIPCION,
            //    Fecha = l.FECHA.Value.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US")),
            //    Usuario = l.USUARIO,
            //    ValAnterior = l.VALOR_ANTERIOR,
            //    ValPosterior = l.VALOR_POSTERIOR
            //}).ToList();
            return Json(new
            {
                success = true,
                ID = newList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
