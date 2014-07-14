﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class SICACIEntities : DbContext
    {
        public SICACIEntities()
            : base("name=SICACIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ROLE> ROLES { get; set; }
        public DbSet<SYSTEM_MENUS> SYSTEM_MENUS { get; set; }
        public DbSet<USUARIO> USUARIOS { get; set; }
    
        public virtual ObjectResult<Nullable<int>> SP_LOGIN_USUARIO(string usuario, string pass)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("pass", pass) :
                new ObjectParameter("pass", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_LOGIN_USUARIO", usuarioParameter, passParameter);
        }
    
        public virtual ObjectResult<string> SP_ROL_BYUSER(string usuario)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_ROL_BYUSER", usuarioParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SP_ISUSER_INROLE(string usuario, string tipo_rol)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var tipo_rolParameter = tipo_rol != null ?
                new ObjectParameter("tipo_rol", tipo_rol) :
                new ObjectParameter("tipo_rol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_ISUSER_INROLE", usuarioParameter, tipo_rolParameter);
        }
    }
}
