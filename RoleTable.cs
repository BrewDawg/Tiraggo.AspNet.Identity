using BusinessObjects;
using System;
using System.Collections.Generic;

namespace Tiraggo.AspNet.Identity
{
    /// <summary>
    /// Class that represents the Role table 
    /// </summary>
    public class RoleTable 
    {
        private string _connectionName;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="database"></param>
        public RoleTable(string connectionName = null)
        {
            _connectionName = connectionName;
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public int Delete(string roleId)
        {
            try
            {
                AspNetRoles.Delete(roleId);
            }
            catch { }

            return 1;
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns></returns>
        public int Insert(IdentityRole role)
        {
            AspNetRoles newRole = new AspNetRoles();
            newRole.Id = role.Id;
            newRole.Name = role.Name;
            newRole.Save();

            return 1;
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(string roleId)
        {
            string name = null;

            AspNetRoles role = new AspNetRoles();
            if(role.LoadByPrimaryKey(roleId))
            {
                name = role.Name;
            }

            return role.Name;
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public string GetRoleId(string roleName)
        {
            string roleId = null;

            AspNetRolesQuery q = new AspNetRolesQuery();
            q.Select(q.Id);
            q.Where(q.Name == roleName);

            AspNetRoles role = new AspNetRoles();
            if(role.Load(q))
            {
                roleId = role.Id;
            }

            return roleId;
        }

        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if(roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        public int Update(IdentityRole role)
        {
            AspNetRoles roleToUpdate = new AspNetRoles();
            roleToUpdate.AcceptChanges();

            roleToUpdate.Id = role.Id;
            roleToUpdate.Name = role.Name;
            roleToUpdate.Save();

            return 1;
        }
    }
}
