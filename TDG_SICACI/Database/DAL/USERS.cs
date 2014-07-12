using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Database.DAL
{
    public interface IUsers {
        bool ValidarUsuario(string user, string pwd);

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
    }
}