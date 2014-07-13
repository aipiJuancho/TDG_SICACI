using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Database.DAL
{
    public interface IUsers {
        bool ValidarUsuario(string user, string pwd);
        bool IsUserinRol(string user, string rol);
        string GetRoles_ByUser(string user);

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
        public bool IsUserinRol(string user, string rol)
        {
            int? iResult = 0;
            try
            {
                using (SICACIEntities cnn = new SICACIEntities())
                {
                    //iResult = cnn.SP_LOGIN_USUARIO(user, pwd).SingleOrDefault();
                    
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
        public string GetRoles_ByUser(string user)
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
    }
}