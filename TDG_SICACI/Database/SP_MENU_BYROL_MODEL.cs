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
    
    public partial class SP_MENU_BYROL_MODEL
    {
        public int ID_ROL { get; set; }
        public int ID_MENU { get; set; }
        public string TITULO_MENU { get; set; }
        public string URL_MENU { get; set; }
        public Nullable<int> ID_PARENT_MENU { get; set; }
        public int ORDEN_MENU { get; set; }
        public Nullable<bool> SHOW_LINEUP { get; set; }
    }
}