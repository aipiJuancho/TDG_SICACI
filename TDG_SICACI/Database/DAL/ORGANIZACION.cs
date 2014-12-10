using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Database.DAL
{
    public interface IOrganizacion
    {
        SP_GET_INFO_ORGANIZACION_MODEL GetInfoOrganizacion();
        IEnumerable<SP_GET_VALORES_ORGANIZACION_MODEL> GetValores();
        IEnumerable<SP_GET_POLITICAS_ORGANIZACION_MODEL> GetPoliticasObjetivos();
        void ModificarOrganizacion(string usuario, string nombre, string eslogan, string alcance, string mision, string vision, DataTable Valores, DataTable Politicas, DataTable Objetivos, string logo);
        IEnumerable<SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES_MODEL> GetPoliticasObjetivos_Vigentes();
        IEnumerable<SP_CONSULTAR_VERSIONES_ANTERIORES_ORGANIZACION_MODEL> VersionesAnteriores();
    }


    public partial class SICACI_DAL : IOrganizacion
    {
        public IOrganizacion IOrganizacion
        {
            get
            {
                return (IOrganizacion)this;
            }
        }

        SP_GET_INFO_ORGANIZACION_MODEL IOrganizacion.GetInfoOrganizacion()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_INFO_ORGANIZACION().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GET_VALORES_ORGANIZACION_MODEL> IOrganizacion.GetValores()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_VALORES_ORGANIZACION().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GET_POLITICAS_ORGANIZACION_MODEL> IOrganizacion.GetPoliticasObjetivos()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_POLITICAS_ORGANIZACION().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }

        void IOrganizacion.ModificarOrganizacion(string usuario, string nombre, string eslogan, string alcance, string mision, string vision, DataTable Valores, DataTable Politicas, DataTable Objetivos, string logo)
        {
            /*STEP 1: Definimos todos los parametros que lleva el SP en la base de datos*/
            SqlParameter parm_Usuario = new SqlParameter("USUARIO", SqlDbType.VarChar, 12);
            SqlParameter parm_NombreOrg = new SqlParameter("NOMBRE_ORG", SqlDbType.VarChar, 50);
            SqlParameter parm_Eslogan = new SqlParameter("ESLOGAN", SqlDbType.VarChar, 200);
            SqlParameter parm_Alcance = new SqlParameter("ALCANCE", SqlDbType.VarChar, 500);
            SqlParameter parm_Mision = new SqlParameter("MISION", SqlDbType.VarChar, 500);
            SqlParameter parm_Vision = new SqlParameter("VISION", SqlDbType.VarChar, 500);
            SqlParameter parm_Valores = new SqlParameter("VALORES", SqlDbType.Structured);
            SqlParameter parm_Politicas = new SqlParameter("POLITICAS", SqlDbType.Structured);
            SqlParameter parm_Objetivos = new SqlParameter("OBJETIVOS", SqlDbType.Structured);
            SqlParameter parm_Logo = new SqlParameter("LOGO", SqlDbType.VarChar, 100);

            /*STEP 2: Preparamos los datos enviados por el controlador en los parametros del SP*/
            parm_Usuario.Value = usuario;
            parm_NombreOrg.Value = nombre;
            parm_Eslogan.Value = eslogan;
            parm_Alcance.Value = alcance;
            parm_Mision.Value = mision;
            parm_Vision.Value = vision;
            parm_Valores.TypeName = "dbo.Valores_Org";
            parm_Valores.Value = Valores;
            parm_Politicas.TypeName = "dbo.Politicas_Org";
            parm_Politicas.Value = Politicas;
            parm_Objetivos.TypeName = "dbo.Objetivos_Org";
            parm_Objetivos.Value = Objetivos;
            parm_Logo.Value = (string.IsNullOrEmpty(logo) ? (object)DBNull.Value : logo);

            /*STEP 4: Enviamos el comando al servidor de BD*/
            SICACIEntities cnn = new SICACIEntities();
            try
            {
                cnn.Database.ExecuteSqlCommand("SP_Actualizar_Informacion @USUARIO, @NOMBRE_ORG, @ESLOGAN, @ALCANCE, @MISION, @VISION, @VALORES, @POLITICAS, @OBJETIVOS, @LOGO",
                    parm_Usuario, parm_NombreOrg, parm_Eslogan, parm_Alcance, parm_Mision, parm_Vision,
                    parm_Valores, parm_Politicas, parm_Objetivos, parm_Logo);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0}. {1}", "Ocurrio un error al intentar guardar la solución del Self-Assessment", ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES_MODEL> IOrganizacion.GetPoliticasObjetivos_Vigentes()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_POLITICAS_OBJETIVOS_VIGENTES().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_VERSIONES_ANTERIORES_ORGANIZACION_MODEL> IOrganizacion.VersionesAnteriores()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_VERSIONES_ANTERIORES_ORGANIZACION().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }
    }
}