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

namespace TDG_SICACI.Controllers
{
    public class OrganizacionController : BaseController
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol = "Administrador";
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        public ActionResult Index()
        {      
            if (User.IsInRole(kUserRol))
            {
                SICACI_DAL db = new SICACI_DAL();
                var info = db.IOrganizacion.GetInfoOrganizacion();
                var arrValores = db.IOrganizacion.GetValores().Select(v => new Models.Consultar_Valor() { valor = v.TEXT_VALOR, descripcion = v.DESC_VALOR }).ToList();
                var arrPoliticas = db.IOrganizacion.GetPoliticasObjetivos().Where(p => p.ID_OBJETIVO.Equals(0))
                    .Select(p => new Models.Consultar_Politica()
                    {
                        politica = p.TEXT_OBJETIVO,
                        descripcion = p.DESC_POLITICA,
                        Objetivos = db.IOrganizacion.GetPoliticasObjetivos().Where(o => o.ID_OBJETIVO != 0 && o.ID_POLITICA.Equals(p.ID_POLITICA))
                            .Select(o => o.TEXT_OBJETIVO).ToList()
                    }).ToList();

                return View(new Models.Consultar_OrganizacionModel
                {
                    nombre  =   info.NOMBRE_ORG,
                    logo    =   Url.Content(string.Format("/Content/{0}", info.logo)),
                    eslogan =   info.ESLOGAN_ORG, 
                    alcance =   info.ALCANCE_ORG,
                    mision  =   info.MISION_ORG,
                    vision  =   info.VISION_ORG,
                    valores = arrValores,
                    politicas = arrPoliticas
                });
            }
            return new HttpNotFoundResult("No se ha definido la vista para los usuarios no Administradores");
        }
        #endregion
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFHandleExceptionMessage(Order = 1)]
        [Authorize(Roles = "Administrador")]
        public ActionResult Modificar()
        {
            SICACI_DAL db = new SICACI_DAL();
            var info = db.IOrganizacion.GetInfoOrganizacion();
            return View(new Models.Modificar_organizacionModel
            {
                nombre = info.NOMBRE_ORG,
                eslogan = info.ESLOGAN_ORG,
                alcance = info.ALCANCE_ORG,
                mision = info.MISION_ORG,
                vision = info.VISION_ORG,
                valores =  new List<Models.Consultar_Valor>()
                                    {
                                         new Models.Consultar_Valor { valor = "nombre del valor", descripcion =  "descripcion del valor"},
                                         new Models.Consultar_Valor { valor = "nombre del valor", descripcion =  "descripcion del valor"},
                                         new Models.Consultar_Valor { valor = "nombre del valor", descripcion =  "descripcion del valor"},
                                         new Models.Consultar_Valor { valor = "nombre del valor", descripcion =  "descripcion del valor"},
                                         new Models.Consultar_Valor { valor = "nombre del valor", descripcion =  "descripcion del valor"},
                                         new Models.Consultar_Valor { valor = "nombre del valor", descripcion =  "descripcion del valor"}
                                    },
                politicas = new List<Models.Consultar_Politica>()
                                    {
                                         new Models.Consultar_Politica{ politica = "texto de la politica", descripcion =  "descripcion de la politica", Objetivos = new List<string>(){"objetivo 1", "objetivo 2"} },
                                         new Models.Consultar_Politica{ politica = "texto de la politica", descripcion =  "descripcion de la politica", Objetivos = new List<string>(){"objetivo 1", "objetivo 2"} },
                                         new Models.Consultar_Politica{ politica = "texto de la politica", descripcion =  "descripcion de la politica", Objetivos = new List<string>(){"objetivo 1", "objetivo 2"} },
                                         new Models.Consultar_Politica{ politica = "texto de la politica", descripcion =  "descripcion de la politica", Objetivos = new List<string>(){"objetivo 1", "objetivo 2"} },
                                         new Models.Consultar_Politica{ politica = "texto de la politica", descripcion =  "descripcion de la politica", Objetivos = new List<string>(){"objetivo 1", "objetivo 2"} },
                                         new Models.Consultar_Politica{ politica = "texto de la politica", descripcion =  "descripcion de la politica", Objetivos = new List<string>(){"objetivo 1", "objetivo 2"} }
                                    }

            });
        }
        #endregion

    }
}
