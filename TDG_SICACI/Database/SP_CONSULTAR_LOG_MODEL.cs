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
    
    public partial class SP_CONSULTAR_LOG_MODEL
    {
        public int ID { get; set; }
        public string TIPO { get; set; }
        public Nullable<System.DateTime> FECHA { get; set; }
        public string USUARIO { get; set; }
        public string ID_USUARIO { get; set; }
        public string DESCRIPCION { get; set; }
        public string VALOR_ANTERIOR { get; set; }
        public string VALOR_POSTERIOR { get; set; }
    }
}
