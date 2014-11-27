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
        public DbSet<FILEGROUP_VERSIONS> FILEGROUP_VERSIONS { get; set; }
        public DbSet<FILEGROUP> FILEGROUPS { get; set; }
    
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
    
        public virtual int SP_NEW_PREGUNTA_ABIERTA(Nullable<int> es_preg_norma, string usuario, string pregunta, string comentario, string tipo_doc, Nullable<int> cat_pertenece, Nullable<int> orden_visual_padd, string lINK_COMENTARIO)
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
    
            var lINK_COMENTARIOParameter = lINK_COMENTARIO != null ?
                new ObjectParameter("LINK_COMENTARIO", lINK_COMENTARIO) :
                new ObjectParameter("LINK_COMENTARIO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_NEW_PREGUNTA_ABIERTA", es_preg_normaParameter, usuarioParameter, preguntaParameter, comentarioParameter, tipo_docParameter, cat_perteneceParameter, orden_visual_paddParameter, lINK_COMENTARIOParameter);
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
    
        public virtual ObjectResult<SP_GET_INFO_ORGANIZACION_MODEL> SP_GET_INFO_ORGANIZACION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_INFO_ORGANIZACION_MODEL>("SP_GET_INFO_ORGANIZACION");
        }
    
        public virtual ObjectResult<SP_GET_VALORES_ORGANIZACION_MODEL> SP_GET_VALORES_ORGANIZACION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_VALORES_ORGANIZACION_MODEL>("SP_GET_VALORES_ORGANIZACION");
        }
    
        public virtual ObjectResult<SP_GET_POLITICAS_ORGANIZACION_MODEL> SP_GET_POLITICAS_ORGANIZACION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_POLITICAS_ORGANIZACION_MODEL>("SP_GET_POLITICAS_ORGANIZACION");
        }
    
        public virtual ObjectResult<SP_GET_EVALUACIONES_MODEL> SP_GET_EVALUACIONES()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_EVALUACIONES_MODEL>("SP_GET_EVALUACIONES");
        }
    
        public virtual ObjectResult<Nullable<int>> SP_CREAR_FILEGROUP_NAME(string fILEGROUP_NAME)
        {
            var fILEGROUP_NAMEParameter = fILEGROUP_NAME != null ?
                new ObjectParameter("FILEGROUP_NAME", fILEGROUP_NAME) :
                new ObjectParameter("FILEGROUP_NAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_CREAR_FILEGROUP_NAME", fILEGROUP_NAMEParameter);
        }
    
        public virtual ObjectResult<string> SP_CREAR_VERSION_FILEGROUP(Nullable<int> iD_FILEGROUP, string uSUARIO, string fILEGROUP_ETIQUETA, string fILE_EXTENSION)
        {
            var iD_FILEGROUPParameter = iD_FILEGROUP.HasValue ?
                new ObjectParameter("ID_FILEGROUP", iD_FILEGROUP) :
                new ObjectParameter("ID_FILEGROUP", typeof(int));
    
            var uSUARIOParameter = uSUARIO != null ?
                new ObjectParameter("USUARIO", uSUARIO) :
                new ObjectParameter("USUARIO", typeof(string));
    
            var fILEGROUP_ETIQUETAParameter = fILEGROUP_ETIQUETA != null ?
                new ObjectParameter("FILEGROUP_ETIQUETA", fILEGROUP_ETIQUETA) :
                new ObjectParameter("FILEGROUP_ETIQUETA", typeof(string));
    
            var fILE_EXTENSIONParameter = fILE_EXTENSION != null ?
                new ObjectParameter("FILE_EXTENSION", fILE_EXTENSION) :
                new ObjectParameter("FILE_EXTENSION", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_CREAR_VERSION_FILEGROUP", iD_FILEGROUPParameter, uSUARIOParameter, fILEGROUP_ETIQUETAParameter, fILE_EXTENSIONParameter);
        }
    
        public virtual ObjectResult<SP_GRID_FILESGROUP_MODEL> SP_GRID_FILESGROUP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GRID_FILESGROUP_MODEL>("SP_GRID_FILESGROUP");
        }
    
        public virtual ObjectResult<SP_CONSULTAR_LASTFILEGROUPS_MODEL> SP_CONSULTAR_LASTFILEGROUPS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CONSULTAR_LASTFILEGROUPS_MODEL>("SP_CONSULTAR_LASTFILEGROUPS");
        }
    
        public virtual ObjectResult<SP_CONSULTAR_VERSIONES_FILEGROUP_MODEL> SP_CONSULTAR_VERSIONES_FILEGROUP(Nullable<int> iD_FILEGROUP)
        {
            var iD_FILEGROUPParameter = iD_FILEGROUP.HasValue ?
                new ObjectParameter("ID_FILEGROUP", iD_FILEGROUP) :
                new ObjectParameter("ID_FILEGROUP", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CONSULTAR_VERSIONES_FILEGROUP_MODEL>("SP_CONSULTAR_VERSIONES_FILEGROUP", iD_FILEGROUPParameter);
        }
    
        public virtual ObjectResult<SP_CONSULTAR_FILEGROUP_BYVERSION_MODEL> SP_CONSULTAR_FILEGROUP_BYVERSION(Nullable<int> iD_FILEGROUP, Nullable<int> nO_VERSION)
        {
            var iD_FILEGROUPParameter = iD_FILEGROUP.HasValue ?
                new ObjectParameter("ID_FILEGROUP", iD_FILEGROUP) :
                new ObjectParameter("ID_FILEGROUP", typeof(int));
    
            var nO_VERSIONParameter = nO_VERSION.HasValue ?
                new ObjectParameter("NO_VERSION", nO_VERSION) :
                new ObjectParameter("NO_VERSION", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CONSULTAR_FILEGROUP_BYVERSION_MODEL>("SP_CONSULTAR_FILEGROUP_BYVERSION", iD_FILEGROUPParameter, nO_VERSIONParameter);
        }
    
        public virtual int SP_UPDATE_PRIMARY_VERSION_FILEGROUP(Nullable<int> iD_FILEGROUP, Nullable<int> nO_VERSION)
        {
            var iD_FILEGROUPParameter = iD_FILEGROUP.HasValue ?
                new ObjectParameter("ID_FILEGROUP", iD_FILEGROUP) :
                new ObjectParameter("ID_FILEGROUP", typeof(int));
    
            var nO_VERSIONParameter = nO_VERSION.HasValue ?
                new ObjectParameter("NO_VERSION", nO_VERSION) :
                new ObjectParameter("NO_VERSION", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_PRIMARY_VERSION_FILEGROUP", iD_FILEGROUPParameter, nO_VERSIONParameter);
        }
    
        public virtual int SP_NEW_FINDING(Nullable<int> tIPO_NOCONFORMIDAD, string cOMENTARIO, string tIPO_ACCION, string aCCION_SUGERIDA, Nullable<System.DateTime> fECHA_LIMITE, string uSUARIO)
        {
            var tIPO_NOCONFORMIDADParameter = tIPO_NOCONFORMIDAD.HasValue ?
                new ObjectParameter("TIPO_NOCONFORMIDAD", tIPO_NOCONFORMIDAD) :
                new ObjectParameter("TIPO_NOCONFORMIDAD", typeof(int));
    
            var cOMENTARIOParameter = cOMENTARIO != null ?
                new ObjectParameter("COMENTARIO", cOMENTARIO) :
                new ObjectParameter("COMENTARIO", typeof(string));
    
            var tIPO_ACCIONParameter = tIPO_ACCION != null ?
                new ObjectParameter("TIPO_ACCION", tIPO_ACCION) :
                new ObjectParameter("TIPO_ACCION", typeof(string));
    
            var aCCION_SUGERIDAParameter = aCCION_SUGERIDA != null ?
                new ObjectParameter("ACCION_SUGERIDA", aCCION_SUGERIDA) :
                new ObjectParameter("ACCION_SUGERIDA", typeof(string));
    
            var fECHA_LIMITEParameter = fECHA_LIMITE.HasValue ?
                new ObjectParameter("FECHA_LIMITE", fECHA_LIMITE) :
                new ObjectParameter("FECHA_LIMITE", typeof(System.DateTime));
    
            var uSUARIOParameter = uSUARIO != null ?
                new ObjectParameter("USUARIO", uSUARIO) :
                new ObjectParameter("USUARIO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_NEW_FINDING", tIPO_NOCONFORMIDADParameter, cOMENTARIOParameter, tIPO_ACCIONParameter, aCCION_SUGERIDAParameter, fECHA_LIMITEParameter, uSUARIOParameter);
        }
    
        public virtual ObjectResult<SP_CONSULTAR_TODOS_FINDINGS_MODEL> SP_CONSULTAR_TODOS_FINDINGS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CONSULTAR_TODOS_FINDINGS_MODEL>("SP_CONSULTAR_TODOS_FINDINGS");
        }
    
        public virtual int SP_UPDATE_FINDING(Nullable<int> iD, Nullable<int> tIPO_NOCONFORMIDAD, string cOMENTARIO, string tIPO_ACCION, string aCCION_SUGERIDA, Nullable<System.DateTime> fECHA_LIMITE)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var tIPO_NOCONFORMIDADParameter = tIPO_NOCONFORMIDAD.HasValue ?
                new ObjectParameter("TIPO_NOCONFORMIDAD", tIPO_NOCONFORMIDAD) :
                new ObjectParameter("TIPO_NOCONFORMIDAD", typeof(int));
    
            var cOMENTARIOParameter = cOMENTARIO != null ?
                new ObjectParameter("COMENTARIO", cOMENTARIO) :
                new ObjectParameter("COMENTARIO", typeof(string));
    
            var tIPO_ACCIONParameter = tIPO_ACCION != null ?
                new ObjectParameter("TIPO_ACCION", tIPO_ACCION) :
                new ObjectParameter("TIPO_ACCION", typeof(string));
    
            var aCCION_SUGERIDAParameter = aCCION_SUGERIDA != null ?
                new ObjectParameter("ACCION_SUGERIDA", aCCION_SUGERIDA) :
                new ObjectParameter("ACCION_SUGERIDA", typeof(string));
    
            var fECHA_LIMITEParameter = fECHA_LIMITE.HasValue ?
                new ObjectParameter("FECHA_LIMITE", fECHA_LIMITE) :
                new ObjectParameter("FECHA_LIMITE", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UPDATE_FINDING", iDParameter, tIPO_NOCONFORMIDADParameter, cOMENTARIOParameter, tIPO_ACCIONParameter, aCCION_SUGERIDAParameter, fECHA_LIMITEParameter);
        }
    
        public virtual int SP_DELETE_FINDING(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DELETE_FINDING", iDParameter);
        }
    
        public virtual ObjectResult<SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES_MODEL> SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES_MODEL>("SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES");
        }
    }
}
