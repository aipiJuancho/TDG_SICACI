//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDG_SICACI.Database
{
    using System;
    
    public partial class SP_CONSULTAR_PROYECTOS_MODEL
    {
        public int ID { get; set; }
        public string NOMBRE_PROYECTO { get; set; }
        public string ID_RESP_EJECUCION { get; set; }
        public string RESPONSABLE_EJECUCION { get; set; }
        public string ID_RESP_APROBACION { get; set; }
        public string RESPONSABLE_APROBACION { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public Nullable<System.DateTime> FECHA_FINALIZACION { get; set; }
        public string ID_ESTADO_PROYECTO { get; set; }
        public string ESTADO_PROYECTO { get; set; }
        public string ID_CREADOR_PROYECTO { get; set; }
        public string CREADOR_PROYECTO { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION_PROYECTO { get; set; }
        public Nullable<int> CANTIDAD_TAREAS { get; set; }
        public Nullable<decimal> PROGRESO_TOTAL { get; set; }
        public int PROGRESO_PROYECTO { get; set; }
    }
}
