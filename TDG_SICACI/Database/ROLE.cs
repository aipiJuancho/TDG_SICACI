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
    using System.Collections.Generic;
    
    public partial class ROLE
    {
        public ROLE()
        {
            this.USUARIOS = new HashSet<USUARIO>();
            this.SYSTEM_MENUS = new HashSet<SYSTEM_MENUS>();
        }
    
        public int ID_ROL { get; set; }
        public string TIPO_ROL { get; set; }
    
        public virtual ICollection<USUARIO> USUARIOS { get; set; }
        public virtual ICollection<SYSTEM_MENUS> SYSTEM_MENUS { get; set; }
    }
}
