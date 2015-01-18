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
            var db = new SICACI_DAL();

            return View(new Models.DashboardModel
            {
                FindingsSinResolver = db.IFindings.GetAll().Where(f => f.ESTADO.Equals("Pendiente"))
                    .OrderBy(f => f.FECHA_LIMITE)
                    .Select(f => new Models.FindingSinResolver() {
                        id = f.ID,
                        comentario = f.COMENTARIO,
                        tipoNoConformidad = f.TIPO_NOCONFORMIDAD,
                        tipoCorreccion = f.TIPO_CORRECION,
                        fechaLimite = (f.FECHA_LIMITE.HasValue ? f.FECHA_LIMITE.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty)
                    }).ToList(),
                MisTareasIncompletas = db.IProyectos.ConsultarTareas()
                    .Where(t => t.ID_RESPONSABLE.ToUpper().Equals(User.Identity.Name.ToUpper()) && t.PROGRESO < 1)
                    .OrderBy(t => t.FECHA_FIN_PREVISTA)
                    .Select(t => new Models.MiTareaIncompleta() {
                        id = t.ID_TAREA,
                        titulo = t.TITULO,
                        descripcion = t.DESCRIPCION,
                        fechaFin = (t.FECHA_FIN_PREVISTA.HasValue ? t.FECHA_FIN_PREVISTA.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty),
                        progreso = (t.PROGRESO.HasValue ? ((int)(t.PROGRESO.Value * 100)).ToString() : "0" )
                    }).ToList(),
                MisProyectosSinTerminar = db.IProyectos.Consultar()
                    .Where(p => p.ID_ESTADO_PROYECTO.Equals("PE") && p.ID_RESP_EJECUCION.ToUpper().Equals(User.Identity.Name.ToUpper()))
                    .OrderBy(p => p.FECHA_FINALIZACION)
                    .Select(p => new Models.MiProyectoSinTerminar() {
                        id = p.ID,
                        nombre = p.NOMBRE_PROYECTO,
                        aprobacion = p.ESTADO_PROYECTO,
                        responableAprobacion = p.RESPONSABLE_APROBACION,
                        fechaInicio = p.FECHA_INICIO.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
                        fechaFinalizacion = (p.FECHA_FINALIZACION.HasValue ? p.FECHA_FINALIZACION.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US")) : string.Empty),
                    }).ToList(),
                ResultadosDeEvaluaciones = new List<Models.ResultadoDeEvaluacion> 
                                        {
                                            new Models.ResultadoDeEvaluacion { revision = 99, puntuacion = 40},
                                            new Models.ResultadoDeEvaluacion { revision = 100, puntuacion = 50},
                                            new Models.ResultadoDeEvaluacion { revision = 105, puntuacion = 40},
                                            new Models.ResultadoDeEvaluacion { revision = 109, puntuacion = 90}
                                        },
                EvaluacionesSinRevisar = db.IPreguntas.GetEvaluaciones()
                    .Where(e => !e.fechaRevision.HasValue)
                    .OrderByDescending(e => e.fechaCreacion)
                    .Select(e => new Models.EvaluacionSinRevisar() {
                        id = e.revision,
                        comentario = (string.IsNullOrEmpty(e.comentario) ? "Sin comentario" : e.comentario),
                        fechaCreacion = e.fechaCreacion.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
                        revision = e.revision
                    }).ToList(),
                ProyectosSinAprobar = db.IProyectos.Consultar()
                    .Where(p => p.ID_ESTADO_PROYECTO.Equals("PE") && p.ID_RESP_EJECUCION.ToUpper() != User.Identity.Name.ToUpper())
                    .OrderBy(p => p.FECHA_INICIO)
                    .Select(p => new Models.ProyectoSinAprobar() {
                        nombre = p.NOMBRE_PROYECTO,
                        fechaInicio = p.FECHA_INICIO.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
                        id = p.ID,
                        responableAprobacion = p.RESPONSABLE_APROBACION,
                        responableEjecucion = p.RESPONSABLE_EJECUCION
                    }).ToList(),
                ProyectosEnEjecucion = db.IProyectos.Consultar()
                    .Where(p => p.ID_ESTADO_PROYECTO.Equals("AP") && p.FECHA_INICIO <= DateTime.Today)
                    .OrderBy(p => p.FECHA_INICIO)
                    .Select(p => new Models.ProyectoEnEjecucion() {
                        id = p.ID,
                        nombre = p.NOMBRE_PROYECTO,
                        fechaInicio = p.FECHA_INICIO.ToString("dd/MM/yyyy", new CultureInfo("en-US")),
                        responableEjecucion = p.RESPONSABLE_EJECUCION,
                        progreso = "0"
                    }).ToList()
            });
        }

    }
}
