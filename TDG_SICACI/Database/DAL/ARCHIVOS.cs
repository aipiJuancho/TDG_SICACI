using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Database.DAL
{
    public interface IArchivos
    {
        List<FILEGROUP> Get_FileGroups();
        int Create_FileGroup_Name(string filegroup);
        string Create_Version_FileGroup(int id, string usuario, string etiqueta, string fileextension);
    }


    public partial class SICACI_DAL : IArchivos
    {
        public IArchivos IArchivos
        {
            get
            {
                return (IArchivos)this;
            }
        }

        List<FILEGROUP> IArchivos.Get_FileGroups()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.FILEGROUPS.ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        int IArchivos.Create_FileGroup_Name(string filegroup)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CREAR_FILEGROUP_NAME(filegroup).FirstOrDefault().Value;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        string IArchivos.Create_Version_FileGroup(int id, string usuario, string etiqueta, string fileextension)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CREAR_VERSION_FILEGROUP(id, usuario, etiqueta, fileextension).FirstOrDefault();
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