using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult TopMenuBar()
        {
            string sRolName = Roles.GetRolesForUser(User.Identity.Name).SingleOrDefault();
            if (string.IsNullOrEmpty(sRolName)) throw new ApplicationException("Se ha producido un error al momento de generar la barra de menu. No se ha podido determinar el rol del usario autenticado.");

            //Una vez validado el rol del usuario, devolvemos el Menu al cual se encuentra autorizado
            SICACI_DAL db = new SICACI_DAL();
            return PartialView();
        }

    }
}
