using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Database.DAL
{
    public interface IUsers {
        bool ValidarUsuario(string user, string pwd);
        bool IsUserinRol(string user, string rol);
        string GetRoles_ByUser(string user);
        IEnumerable<SP_MENU_BYROL_MODEL> GetTopMenu(string role);
        string GetName(string user);
        IEnumerable<SP_GET_LISTUSER_MODEL> GetUserList(string status);
        SP_GET_LISTUSER_MODEL GetInfoUser(string usuario);
    }


    public partial class SICACI_DAL : IUsers
    {
        public IUsers IUsers
        {
            get
            {
                return (IUsers)this;
            }
        }

        /// <summary>
        /// Metodo que valida las credenciales del usuario
        /// </summary>
        /// <param name="user">Usuario o Nickname que se desea validar</param>
        /// <param name="pwd">Contraseña asociada a la cuenta</param>
        /// <returns></returns>
        bool IUsers.ValidarUsuario(string user, string pwd)
        {
            int? iResult = 0;
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    iResult = cnn.SP_LOGIN_USUARIO(user, pwd).SingleOrDefault();
                }
                if ((iResult.HasValue) && (iResult == 1)) return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }

            return false;
        }

        /// <summary>
        /// Devuelve los VERDADERO si el usuario especificado pertenece al rol especificado. De lo contrario regresa FALSO
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rol"></param>
        /// <returns></returns>
        bool IUsers.IsUserinRol(string user, string rol)
        {
            int? iResult = 0;
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    iResult = cnn.SP_ISUSER_INROLE(user, rol).SingleOrDefault();
                    
                }
                if ((iResult.HasValue) && (iResult == 1)) return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
            return false;
        }

        /// <summary>
        /// Obtiene el Rol del usuario actual
        /// </summary>
        /// <param name="user">Usuario del cual se desea obtener el rol al que pertenece</param>
        /// <returns></returns>
        string IUsers.GetRoles_ByUser(string user)
        {
            var sRol = string.Empty;
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    sRol = cnn.SP_ROL_BYUSER(user).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
            return sRol;
        }


        IEnumerable<SP_MENU_BYROL_MODEL> IUsers.GetTopMenu(string role)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_MENU_BYROL(role).Where(m => m.ID_PARENT_MENU == null).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        string IUsers.GetName(string user)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_NAMES(user).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        IEnumerable<SP_GET_LISTUSER_MODEL> IUsers.GetUserList(string status)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_GET_LISTADO_USUARIOS(status).ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException) throw ex.InnerException;
                throw new Exception(string.Format("{0} {1}", JertiFramework.My.Resources.JFLibraryErrors.Error_Try_Catch_Server, ex.Message), ex);
            }
        }


        SP_GET_LISTUSER_MODEL IUsers.GetInfoUser(string usuario)
        {
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    return cnn.SP_INFO_USUARIO(usuario).SingleOrDefault();
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