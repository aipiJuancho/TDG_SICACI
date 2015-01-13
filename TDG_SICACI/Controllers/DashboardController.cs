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
using System.Data;
using System.IO;
using System.Globalization;

namespace TDG_SICACI.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /SelfA/

        public ActionResult Index()
        {
            return View(new Models.DashboardModel
            {
                FindingsSinResolver = new List<Models.FindingSinResolver> 
                                        {
                                            new Models.FindingSinResolver{ tipoNoConformidad="Oportunidad de mejora", tipoCorreccion="Sostenible", fechaLimite = "11/11/2014", comentario = "Comentario del finding", id = 2},
                                            new Models.FindingSinResolver{ tipoNoConformidad="Oportunidad de mejora", tipoCorreccion="Inmediata", fechaLimite = "11/11/2014", comentario = "Comentario del finding", id = 2},
                                            new Models.FindingSinResolver{ tipoNoConformidad="Oportunidad de mejora", tipoCorreccion="Inmediata", fechaLimite = "11/11/2014", comentario = "Comentario del finding", id = 2},
                                            new Models.FindingSinResolver{ tipoNoConformidad="Oportunidad de mejora", tipoCorreccion="Inmediata", fechaLimite = "11/11/2014", comentario = "Comentario del finding", id = 2},
                                            new Models.FindingSinResolver{ tipoNoConformidad="Oportunidad de mejora", tipoCorreccion="Inmediata", fechaLimite = "11/11/2014", comentario = "Comentario del finding", id = 2},
                                            new Models.FindingSinResolver{ tipoNoConformidad="Oportunidad de mejora", tipoCorreccion="Inmediata", fechaLimite = "11/11/2014", comentario = "Comentario del finding", id = 2}
                                        }
            });
        }

    }
}
