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

namespace TDG_SICACI.Controllers
{
    public class OrganizacionController : BaseController
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region constants
        private const string kUserRol = "Administrador,RD";
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Manage
        [HttpGet()]
        public ActionResult Index()
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

                var arrVersiones = db.IOrganizacion.VersionesAnteriores().Select(v => new Models.Consultar_Versiones()
                {
                    id_Version = v.ID_INFORMACION,
                    usuario = v.USUARIO,
                    fecha_Version = v.FECHA_INFORMACION.ToString("dd/MM/yyyy hh:mm tt", new System.Globalization.CultureInfo("en-US"))
                }).ToList();

                ViewBag.ShowButton_Modify = (User.IsInRole("Administrador") || User.IsInRole("RD") ? true : false);
                return View(new Models.Consultar_OrganizacionModel
                {
                    nombre = info.NOMBRE_ORG,
                    logo = Url.Content(string.Format("/Content/{0}", info.logo)),
                    eslogan = info.ESLOGAN_ORG,
                    alcance = info.ALCANCE_ORG,
                    mision = info.MISION_ORG,
                    vision = info.VISION_ORG,
                    valores = arrValores,
                    politicas = arrPoliticas,
                    versiones = arrVersiones
                });
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region read
        [HttpGet()]
        public ActionResult Consultar(int version = 0)
        {
            //Debemos validar que se haya pasado un usuario en la solicitud
            if (version == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.ErrorMessage = "ERROR! No se especifico en la solicitud la versión que se desea consultar.";
                return View("Error");
            }

            SICACI_DAL db = new SICACI_DAL();
            if (db.IOrganizacion.VersionesAnteriores().Where(v => v.ID_INFORMACION.Equals(version)).Count() == 0)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.ErrorMessage = "ERROR! La versión especificada no se encontro en el sistema.";
                return View("Error");
            }


            var info = db.IOrganizacion.GetInfoOrganizacion_Versiones().Where(v => v.ID_INFORMACION.Equals(version)).FirstOrDefault();
            var arrValores = db.IOrganizacion.GetValores_Versiones()
                .Where(v => v.ID_INFORMACION.Equals(version))
                .Select(v => new Models.Consultar_Valor() { valor = v.TEXT_VALOR, descripcion = v.DESC_VALOR }).ToList();
            var arrPoliticas = db.IOrganizacion.GetPoliticasObjetivos_Versiones()
                    .Where(p => p.ID_OBJETIVO.Equals(0) && p.ID_INFORMACION.Equals(version))
                    .Select(p => new Models.Consultar_Politica()
                    {
                        politica = p.TEXT_OBJETIVO,
                        descripcion = p.DESC_POLITICA,
                        Objetivos = db.IOrganizacion.GetPoliticasObjetivos_Versiones()
                            .Where(o => o.ID_OBJETIVO != 0 && o.ID_POLITICA.Equals(p.ID_POLITICA) && o.ID_INFORMACION.Equals(version))
                            .Select(o => o.TEXT_OBJETIVO).ToList()
                    }).ToList();

            var arrVersiones = db.IOrganizacion.VersionesAnteriores().Select(v => new Models.Consultar_Versiones()
            {
                id_Version = v.ID_INFORMACION,
                usuario = v.USUARIO,
                fecha_Version = v.FECHA_INFORMACION.ToString("dd/MM/yyyy hh:mm tt", new System.Globalization.CultureInfo("en-US"))
            }).ToList();

            return View(new Models.Consultar_OrganizacionModel
            {
                nombre = info.NOMBRE_ORG,
                logo = Url.Content(string.Format("/Content/{0}", info.logo)),
                eslogan = info.ESLOGAN_ORG,
                alcance = info.ALCANCE_ORG,
                mision = info.MISION_ORG,
                vision = info.VISION_ORG,
                valores = arrValores,
                politicas = arrPoliticas,
                versiones = arrVersiones,
                idVersionSeleccionada = version
            });
            //}
            //else 
            //{ 
            // return new HttpNotFoundResult("No se ha definido la version a consultar");
            //}
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Update
        [HttpGet()]
        [JFAutorizationSecurity(Roles=kUserRol)]
        [JFHandleExceptionMessage(Order = 1)]
        public ActionResult Modificar()
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

            return View(new Models.Modificar_organizacionModel
            {
                nombre = info.NOMBRE_ORG,
                eslogan = info.ESLOGAN_ORG,
                alcance = info.ALCANCE_ORG,
                mision = info.MISION_ORG,
                vision = info.VISION_ORG,
                valores = arrValores,
                politicas = arrPoliticas
            });
        }

        [HttpPost()]
        [JFHandleExceptionMessage(Order = 1)]
        [JFAutorizationSecurity(Roles = kUserRol)]
        public JsonResult _save_organizacion(Models.ModificarOrganizacionModel model)
        {
            //Hacemos unas validaciones en los datos recibidos
            if ((model.Valor_Descripcion.Count().Equals(0)) || (model.Politica_Descripcion.Count().Equals(0)) || (model.Politica_Objetivos.Count().Equals(0)))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Json(new
                {
                    notify = new JFNotifySystemMessage("Se han encontrado una serie de problemas en las estructuras de datos Valores, Politicas y/u Objetivos.", titulo: "Formulario Incompleto", permanente: false, tiempo: 5000)
                }, JsonRequestBehavior.AllowGet);
            }

            /*Preparamos las 3 tablas que deberán enviarse al servidor*/
            DataTable udt_Valores = new DataTable();
            udt_Valores.Columns.Add("ID_VALOR", typeof(int));
            udt_Valores.Columns.Add("TEXT_VALOR", typeof(string));
            udt_Valores.Columns.Add("DESC_VALOR", typeof(string));
            for (int i = 0; i < model.Valor_Descripcion.Count(); i++)
            {
                udt_Valores.Rows.Add(0, model.Valor_Texto.ElementAt(i), model.Valor_Descripcion.ElementAt(i));
            }

            DataTable udt_Politicas = new DataTable();
            udt_Politicas.Columns.Add("ID_POLITICA", typeof(int));
            udt_Politicas.Columns.Add("TEXT_POLITICA", typeof(string));
            udt_Politicas.Columns.Add("DESC_POLITICA", typeof(string));
            for (int i = 0; i < model.Politica_Descripcion.Count(); i++)
            {
                udt_Politicas.Rows.Add(0, model.Politica_Texto.ElementAt(i), 
                    model.Politica_Descripcion.ElementAt(i));
            }

            DataTable udt_Objetivos = new DataTable();
            udt_Objetivos.Columns.Add("ID_OBJETIVO", typeof(int));
            udt_Objetivos.Columns.Add("TEXT_OBJETIVO", typeof(string));
            udt_Objetivos.Columns.Add("ID_POLITICA", typeof(int));
            udt_Objetivos.Columns.Add("TEXT_POLITICA", typeof(string));
            for (int i = 0; i < model.Politica_Objetivos.Count(); i++)
            {
                udt_Objetivos.Rows.Add(0, model.Politica_Objetivos.ElementAt(i), 0,
                    model.Politica_Objetivos_TextPolitica.ElementAt(i));
            }

            /*Verificamos si se ha cargado algun archivo*/
            string fileName = string.Empty;
            if (model.logo != null)
            {
                if (model.logo.ContentLength > 0)
                {
                    if (model.logo.ContentType.Contains("image/"))  //Verificamos que sea un archivo de Imagen
                    {
                        var arrFile = model.logo.FileName.Split(new char[] { '.' });
                        fileName = string.Format("logo.{0}", arrFile.ElementAt(arrFile.Length - 1));
                        var path = Path.Combine(Server.MapPath("~/Content"), fileName);
                        model.logo.SaveAs(path);
                    }
                }
            }

            SICACI_DAL db = new SICACI_DAL();
            db.IOrganizacion.ModificarOrganizacion(User.Identity.Name, model.nombre, model.eslogan,
                   model.alcance, model.mision, model.vision, udt_Valores, udt_Politicas, udt_Objetivos,
                   fileName);
            

            return Json(new
            {
                success = true,
                redirectURL = Url.Action("Index", "Organizacion")
            });
        }
        #endregion

    }
}
