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
    
    public partial class USUARIO
    {
        public USUARIO()
        {
            this.FILEGROUP_VERSIONS = new HashSet<FILEGROUP_VERSIONS>();
        }
    
        public string USUARIO1 { get; set; }
        public int ID_ROL { get; set; }
        public byte[] PASSWORD { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public int ACTIVO { get; set; }
    
        public virtual ROLE ROLE { get; set; }
        public virtual ICollection<FILEGROUP_VERSIONS> FILEGROUP_VERSIONS { get; set; }
    }
}
