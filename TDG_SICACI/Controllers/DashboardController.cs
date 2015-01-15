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
                                        },
                MisTareasIncompletas = new List<Models.MiTareaIncompleta> 
                                        {
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8},
                                            new Models.MiTareaIncompleta { titulo = "Titulo de la tarea", descripcion="Descripcion de la tarea", progreso="30", fechaFin = "28/12/2015", id = 8}
                                        },
                MisProyectosSinTerminar = new List<Models.MiProyectoSinTerminar> 
                                        {
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 },
                                            new Models.MiProyectoSinTerminar{ nombre = "Nombre del proyecto", aprobacion = "Pendiente Aprobacion", responableAprobacion = "Sofy", progreso="0", fechaInicio = "1/1/2015", fechaFinalizacion = "31/12/2015", id = 10 }
                                        },
                ResultadosDeEvaluaciones = new List<Models.ResultadoDeEvaluacion> 
                                        {
                                            new Models.ResultadoDeEvaluacion { revision = 99, puntuacion = 40},
                                            new Models.ResultadoDeEvaluacion { revision = 100, puntuacion = 50},
                                            new Models.ResultadoDeEvaluacion { revision = 105, puntuacion = 40},
                                            new Models.ResultadoDeEvaluacion { revision = 109, puntuacion = 90}
                                        },
                EvaluacionesSinRevisar = new List<Models.EvaluacionSinRevisar> 
                                        {
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                            new Models.EvaluacionSinRevisar{ fechaCreacion = "1/1/11", comentario ="Comentario de la evaluacion", revision = 99, id = 9},
                                        },
                ProyectosSinAprobar = new List<Models.ProyectoSinAprobar> 
                                        {
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                            new Models.ProyectoSinAprobar{ nombre = "Nombre del proyecto", fechaInicio = "3/3/15", responableEjecucion = "Juan", responableAprobacion = "Sofy", id = 10},
                                        },
                ProyectosEnEjecucion = new List<Models.ProyectoEnEjecucion> 
                                        {
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                            new Models.ProyectoEnEjecucion{ nombre = "Nombre del proyecto", fechaInicio ="9/9/99", responableEjecucion = "Nelson", progreso ="70", id = 10},
                                        }
            });
        }

    }
}
