using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

namespace TDG_SICACI.Models
{
    public class DashboardModel
    {
        public List<FindingSinResolver>     FindingsSinResolver         { get; set; }
        public List<MiTareaIncompleta>      MisTareasIncompletas        { get; set; }
        public List<MiProyectoSinTerminar>  MisProyectosSinTerminar     { get; set; }
        public List<ResultadoDeEvaluacion>  ResultadosDeEvaluaciones    { get; set; }
        public List<EvaluacionSinRevisar>   EvaluacionesSinRevisar      { get; set; }
        public List<ProyectoSinAprobar>     ProyectosSinAprobar         { get; set; }
        public List<ProyectoEnEjecucion>    ProyectosEnEjecucion        { get; set; }        
    }

    public class ProyectoEnEjecucion
    {
        public string nombre                { get; set; }
        public string responableEjecucion   { get; set; }
        public string fechaInicio           { get; set; }
        public string progreso              { get; set; }
        public int id                       { get; set; }
    }

    public class ProyectoSinAprobar
    {
        public string nombre                { get; set; }
        public string responableAprobacion  { get; set; }
        public string responableEjecucion   { get; set; }
        public string fechaInicio           { get; set; }
        public int id                       { get; set; }
    }

    public class EvaluacionSinRevisar
    {
        public string fechaCreacion     { get; set; }
        public int revision             { get; set; }
        public string comentario        { get; set; }
        public int id                   { get; set; }

    }

    public class ResultadoDeEvaluacion
    {
        public int revision     { get; set; }
        public int puntuacion   { get; set; }
    }

    public class MiProyectoSinTerminar
    {
        public string nombre                { get; set; }//
        public string responableAprobacion  { get; set; }//
        public string fechaInicio           { get; set; }//
        public string fechaFinalizacion     { get; set; }//
        public string progreso              { get; set; }//
        public string aprobacion            { get; set; }//
        public int id                       { get; set; }//

    }

    public class MiTareaIncompleta
    {
        public string titulo        { get; set; }
        public string descripcion   { get; set; }
        public string fechaFin      { get; set; }
        public string progreso      { get; set; }
        public int id               { get; set; }
    }

    public class FindingSinResolver
    {
        public string tipoNoConformidad { get; set; }
        public string tipoCorreccion    { get; set; }
        public string comentario        { get; set; }
        public string fechaLimite       { get; set; }
        public int id                   { get; set; }

    }

}