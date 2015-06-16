using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TDG_SICACI.Database.DAL;

namespace TDG_SICACI.Providers
{
    public class TDGRoleProvider : RoleProvider
    {
        private SICACI_DAL DAL;
        public TDGRoleProvider()
        {
            DAL = new SICACI_DAL();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                if ((string.IsNullOrEmpty(username)) || (string.IsNullOrEmpty(roleName))) return false;
                return DAL.IUsers.IsUserinRol(username, roleName);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            var sRol = string.Empty;
            if (string.IsNullOrEmpty(username)) return new string[0];
            try
            {
                sRol = DAL.IUsers.GetRoles_ByUser(username);
            }
            catch (Exception ex) {
                return new string[0];
            }
            return new string[] { sRol };
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}