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
    }
}