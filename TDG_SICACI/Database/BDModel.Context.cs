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
    
        public virtual ObjectResult<SP_MENU_BYROL_MODEL> SP_GET_MENU_BYROL(string tipo_rol)
        {
            var tipo_rolParameter = tipo_rol != null ?
                new ObjectParameter("tipo_rol", tipo_rol) :
                new ObjectParameter("tipo_rol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_MENU_BYROL_MODEL>("SP_GET_MENU_BYROL", tipo_rolParameter);
        }
    
        public virtual ObjectResult<string> SP_GET_NAMES(string usuario)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_GET_NAMES", usuarioParameter);
        }
    
        public virtual ObjectResult<SP_GET_LISTUSER_MODEL> SP_GET_LISTADO_USUARIOS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LISTUSER_MODEL>("SP_GET_LISTADO_USUARIOS");
        }
    
        public virtual ObjectResult<SP_GET_LISTUSER_MODEL> SP_INFO_USUARIO(string usuario)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LISTUSER_MODEL>("SP_INFO_USUARIO", usuarioParameter);
        }
    
        public virtual int SP_DELETE_USER(string usuario)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_USER", usuarioParameter);
        }
    
        public virtual int SP_UPDATE_UER(string usuario, Nullable<int> id_rol, string nombres, string apellidos, string correo, string activo)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var id_rolParameter = id_rol.HasValue ?
                new ObjectParameter("id_rol", id_rol) :
                new ObjectParameter("id_rol", typeof(int));
    
            var nombresParameter = nombres != null ?
                new ObjectParameter("nombres", nombres) :
                new ObjectParameter("nombres", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("apellidos", apellidos) :
                new ObjectParameter("apellidos", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var activoParameter = activo != null ?
                new ObjectParameter("activo", activo) :
                new ObjectParameter("activo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_UER", usuarioParameter, id_rolParameter, nombresParameter, apellidosParameter, correoParameter, activoParameter);
        }
    
        public virtual int SP_NEW_USER(string usuario, Nullable<int> id_rol, string pass, string nombres, string apellidos, string correo, string activo)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var id_rolParameter = id_rol.HasValue ?
                new ObjectParameter("id_rol", id_rol) :
                new ObjectParameter("id_rol", typeof(int));
    
            var passParameter = pass != null ?
                new ObjectParameter("pass", pass) :
                new ObjectParameter("pass", typeof(string));
    
            var nombresParameter = nombres != null ?
                new ObjectParameter("nombres", nombres) :
                new ObjectParameter("nombres", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("apellidos", apellidos) :
                new ObjectParameter("apellidos", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var activoParameter = activo != null ?
                new ObjectParameter("activo", activo) :
                new ObjectParameter("activo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_NEW_USER", usuarioParameter, id_rolParameter, passParameter, nombresParameter, apellidosParameter, correoParameter, activoParameter);
        }
    
        public virtual int SP_CHANGE_PASSWORD(string usuario, string pass, string pass_old)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("pass", pass) :
                new ObjectParameter("pass", typeof(string));
    
            var pass_oldParameter = pass_old != null ?
                new ObjectParameter("pass_old", pass_old) :
                new ObjectParameter("pass_old", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_CHANGE_PASSWORD", usuarioParameter, passParameter, pass_oldParameter);
        }
    
        public virtual ObjectResult<SP_GET_LISTPREGUNTA_MODEL> SP_GET_PREGUNTAS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LISTPREGUNTA_MODEL>("SP_GET_PREGUNTAS");
        }
    
        public virtual ObjectResult<SP_GET_NORMA_ISO_MODEL> SP_GET_NORMA_ISO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_NORMA_ISO_MODEL>("SP_GET_NORMA_ISO");
        }
    
        public virtual int SP_NEW_PREGUNTA_ABIERTA(Nullable<int> es_preg_norma, string usuario, string pregunta, string comentario, string tipo_doc, Nullable<int> cat_pertenece, Nullable<int> orden_visual_padd)
        {
            var es_preg_normaParameter = es_preg_norma.HasValue ?
                new ObjectParameter("es_preg_norma", es_preg_norma) :
                new ObjectParameter("es_preg_norma", typeof(int));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var preguntaParameter = pregunta != null ?
                new ObjectParameter("pregunta", pregunta) :
                new ObjectParameter("pregunta", typeof(string));
    
            var comentarioParameter = comentario != null ?
                new ObjectParameter("comentario", comentario) :
                new ObjectParameter("comentario", typeof(string));
    
            var tipo_docParameter = tipo_doc != null ?
                new ObjectParameter("tipo_doc", tipo_doc) :
                new ObjectParameter("tipo_doc", typeof(string));
    
            var cat_perteneceParameter = cat_pertenece.HasValue ?
                new ObjectParameter("cat_pertenece", cat_pertenece) :
                new ObjectParameter("cat_pertenece", typeof(int));
    
            var orden_visual_paddParameter = orden_visual_padd.HasValue ?
                new ObjectParameter("orden_visual_padd", orden_visual_padd) :
                new ObjectParameter("orden_visual_padd", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_NEW_PREGUNTA_ABIERTA", es_preg_normaParameter, usuarioParameter, preguntaParameter, comentarioParameter, tipo_docParameter, cat_perteneceParameter, orden_visual_paddParameter);
        }
    
        public virtual int SP_DELETE_PREGUNTA_GIDEM(Nullable<int> id_preg)
        {
            var id_pregParameter = id_preg.HasValue ?
                new ObjectParameter("id_preg", id_preg) :
                new ObjectParameter("id_preg", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_PREGUNTA_GIDEM", id_pregParameter);
        }
    
        public virtual ObjectResult<SP_CONSTRUIR_SELF_MODEL> SP_CONSTRUIR_SELF()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CONSTRUIR_SELF_MODEL>("SP_CONSTRUIR_SELF");
        }
    
        public virtual int SP_UPDATE_PREGUNTA_ADD(Nullable<int> id_preg, Nullable<int> orden_visual)
        {
            var id_pregParameter = id_preg.HasValue ?
                new ObjectParameter("id_preg", id_preg) :
                new ObjectParameter("id_preg", typeof(int));
    
            var orden_visualParameter = orden_visual.HasValue ?
                new ObjectParameter("orden_visual", orden_visual) :
                new ObjectParameter("orden_visual", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_PREGUNTA_ADD", id_pregParameter, orden_visualParameter);
        }
    
        public virtual ObjectResult<SP_GET_PREGUNTA_MODEL> SP_GET_PREGUNTA(Nullable<int> id_preg)
        {
            var id_pregParameter = id_preg.HasValue ?
                new ObjectParameter("id_preg", id_preg) :
                new ObjectParameter("id_preg", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_PREGUNTA_MODEL>("SP_GET_PREGUNTA", id_pregParameter);
        }
    
        public virtual int SP_ASOCIAR_DOCUMENTO_RESPUESTA(Nullable<int> iD_SOLUCION, Nullable<int> iD_PREGUNTA, string aRCHIVO)
        {
            var iD_SOLUCIONParameter = iD_SOLUCION.HasValue ?
                new ObjectParameter("ID_SOLUCION", iD_SOLUCION) :
                new ObjectParameter("ID_SOLUCION", typeof(int));
    
            var iD_PREGUNTAParameter = iD_PREGUNTA.HasValue ?
                new ObjectParameter("ID_PREGUNTA", iD_PREGUNTA) :
                new ObjectParameter("ID_PREGUNTA", typeof(int));
    
            var aRCHIVOParameter = aRCHIVO != null ?
                new ObjectParameter("ARCHIVO", aRCHIVO) :
                new ObjectParameter("ARCHIVO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ASOCIAR_DOCUMENTO_RESPUESTA", iD_SOLUCIONParameter, iD_PREGUNTAParameter, aRCHIVOParameter);
        }
    }
}
