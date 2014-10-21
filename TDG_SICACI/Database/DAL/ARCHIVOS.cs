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
        List<SP_GRID_FILESGROUP_MODEL> Grid_FileGroups();
        SP_CONSULTAR_LASTFILEGROUPS_MODEL Get_FileGroup_Last(int id);
        List<SP_CONSULTAR_VERSIONES_FILEGROUP_MODEL> Get_Versions_ByFilegroup(int id);
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


        List<SP_GRID_FILESGROUP_MODEL> IArchivos.Grid_FileGroups()
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GRID_FILESGROUP().ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        SP_CONSULTAR_LASTFILEGROUPS_MODEL IArchivos.Get_FileGroup_Last(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    if (cnn.SP_CONSULTAR_LASTFILEGROUPS().Where(f => f.ID_FILEGROUP.Equals(id)).Count().Equals(0))
                        throw new Exception("Lo sentimos, pero el ID especificado no se encontro en el sistema.");

                    return cnn.SP_CONSULTAR_LASTFILEGROUPS().Where(f => f.ID_FILEGROUP.Equals(id)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        List<SP_CONSULTAR_VERSIONES_FILEGROUP_MODEL> IArchivos.Get_Versions_ByFilegroup(int id)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_CONSULTAR_VERSIONES_FILEGROUP(id).ToList();
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