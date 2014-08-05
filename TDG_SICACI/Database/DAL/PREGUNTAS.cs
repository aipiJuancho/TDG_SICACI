using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Database.DAL
{
    public interface IPreguntas
    {
        IEnumerable<SP_GET_LISTPREGUNTA_MODEL> GetPreguntaList();
        IEnumerable<SP_GET_NORMA_ISO_MODEL> GetNormaISO();
    }


    public partial class SICACI_DAL : IPreguntas
    {
        public IPreguntas IPreguntas
        {
            get
            {
                return (IPreguntas)this;
            }
        }

        IEnumerable<SP_GET_LISTPREGUNTA_MODEL> IPreguntas.GetPreguntaList()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_PREGUNTAS().ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }

        IEnumerable<SP_GET_NORMA_ISO_MODEL> IPreguntas.GetNormaISO()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_NORMA_ISO().ToArray();
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