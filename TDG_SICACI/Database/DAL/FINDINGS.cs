using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Database.DAL
{
    public interface IFindings
    {
        void Create_Finding(int TipoNoConformidad, string Comentario, string TipoAccion, string AccionSugerida, DateTime? FechaSugerida, string Usuario);
        IEnumerable<SP_CONSULTAR_TODOS_FINDINGS_MODEL> GetAll();
        void Update_Finding(int ID, int TipoNoConformidad, string Comentario, string TipoAccion, string AccionSugerida, DateTime? FechaSugerida);
        void Delete_Finding(int ID);
    }


    public partial class SICACI_DAL : IFindings
    {
        public IFindings IFindings
        {
            get
            {
                return (IFindings)this;
            }
        }

        void IFindings.Create_Finding(int TipoNoConformidad, string Comentario, string TipoAccion, string AccionSugerida, DateTime? FechaSugerida, string Usuario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_NEW_FINDING(TipoNoConformidad, Comentario, TipoAccion, AccionSugerida, FechaSugerida, Usuario);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_CONSULTAR_TODOS_FINDINGS_MODEL> IFindings.GetAll()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_TODOS_FINDINGS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }
    

        void IFindings.Update_Finding(int ID, int TipoNoConformidad, string Comentario, string TipoAccion, string AccionSugerida, DateTime? FechaSugerida)
        {
 	        try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_UPDATE_FINDING(ID, TipoNoConformidad, Comentario, TipoAccion, AccionSugerida, FechaSugerida);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        void IFindings.Delete_Finding(int ID)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_DELETE_FINDING(ID);
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