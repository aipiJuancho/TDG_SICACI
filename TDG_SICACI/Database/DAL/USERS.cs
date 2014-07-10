using System;
using System.Collections.Generic;
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

        bool IUsers.ValidarUsuario(string user, string pwd)
        {
            using (SICACIEntities cnn = new SICACIEntities())
            {
                return true;
            }

            throw new NotImplementedException();
        }
    }
}