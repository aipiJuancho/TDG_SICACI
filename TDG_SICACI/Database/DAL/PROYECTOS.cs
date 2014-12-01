using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Database.DAL
{
    public interface IProyectos
    {
        void NuevoProyecto(string Nombre, string Resp_Ejecucion, string Resp_Aprobacion, string Objetivos, string Findings, DateTime FInicio, string User);
    }


    public partial class SICACI_DAL : IProyectos
    {
        public IProyectos IProyectos
        {
            get
            {
                return (IProyectos)this;
            }
        }
    
        void IProyectos.NuevoProyecto(string Nombre, string Resp_Ejecucion, string Resp_Aprobacion, string Objetivos, string Findings, DateTime FInicio, string User)
        {
 	        try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    cnn.SP_CREAR_PROYECTO(Nombre, Resp_Ejecucion, Resp_Aprobacion, Objetivos, Findings, FInicio, User);
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