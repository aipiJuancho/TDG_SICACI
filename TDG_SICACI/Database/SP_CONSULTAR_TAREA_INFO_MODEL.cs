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
    
    public partial class SP_CONSULTAR_TAREA_INFO_MODEL
    {
        public int ID_TAREA { get; set; }
        public int ID_PROYECTO { get; set; }
        public Nullable<int> ORDEN_VISUAL { get; set; }
        public string TITULO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ID_RESPONSABLE { get; set; }
        public string RESPONSABLE { get; set; }
        public string RECURSOS_ASIGNADOS { get; set; }
        public Nullable<System.DateTime> FECHA_FIN_PREVISTA { get; set; }
        public Nullable<decimal> PROGRESO { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMO_PROGRESO { get; set; }
        public string ID_RESPONSABLE_ULTIMO_CAMBIO_PROGRESO { get; set; }
        public string RESPONSABLE_ULTIMO_CAMBIO_PROGRESO { get; set; }
    }
}