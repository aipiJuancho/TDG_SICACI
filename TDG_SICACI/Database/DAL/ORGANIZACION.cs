using System;
using System.Collections.Generic;
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
    }
}