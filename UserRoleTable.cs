using BusinessObjects;
using System.Collections.Generic;

namespace Tiraggo.AspNet.Identity
{
    /// <summary>
    /// Class that represents the UserRoles table
    /// </summary>
    public class UserRolesTable
    {
        private string _connectionName;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesTable(string connectionName = null)
        {
            _connectionName = connectionName;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(string userId)
        {
            List<string> roles = new List<string>();

            AspNetRolesQuery r = new AspNetRolesQuery("r");
            AspNetUserRolesQuery ur = new AspNetUserRolesQuery("ur");

            r.Select(r.Name);
            r.InnerJoin(ur).On(ur.UserId == userId && ur.RoleId == r.Id);

            AspNetRolesCollection coll = new AspNetRolesCollection();
            if(coll.Load(r))
            {
                foreach(AspNetRoles role in coll)
                {
                    roles.Add(role.Name);
                }
            }

            return roles;
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            try
            {
                AspNetUserRolesQuery q = new AspNetUserRolesQuery();
                q.Where(q.UserId == userId);

                AspNetUserRolesCollection coll = new AspNetUserRolesCollection();
                if(coll.Load(q))
                {
                    coll.MarkAllAsDeleted();
                    coll.Save();
                }
            }
            catch { }

            return 1;
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, string roleId)
        {
            AspNetUserRoles userRole = new AspNetUserRoles();
            userRole.UserId = user.Id;
            userRole.RoleId = roleId;
            userRole.Save();

            return 1;
        }
    }
}
