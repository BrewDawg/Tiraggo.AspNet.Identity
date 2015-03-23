using BusinessObjects;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Tiraggo.AspNet.Identity
{
    /// <summary>
    /// Class that represents the UserLogins table
    /// </summary>
    public class UserLoginsTable
    {
        private string _connectionName;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="database"></param>
        public UserLoginsTable(string connectionString = null)
        {
            _connectionName = connectionString;
        }

        /// <summary>
        /// Deletes a login from a user in the UserLogins table
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, UserLoginInfo login)
        {
            AspNetUserLoginsQuery q = new AspNetUserLoginsQuery();
            q.Where(q.UserId == user.Id && q.LoginProvider == login.LoginProvider && q.ProviderKey == login.ProviderKey);

            AspNetUserLoginsCollection loginUsers = new AspNetUserLoginsCollection();
            if(loginUsers.Load(q))
            {
                loginUsers.MarkAllAsDeleted();
                loginUsers.Save();
            }

            return 1;
        }

        /// <summary>
        /// Deletes all Logins from a user in the UserLogins table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            AspNetUserLoginsQuery q = new AspNetUserLoginsQuery();
            q.Where(q.UserId == userId);

            AspNetUserLoginsCollection loginUsers = new AspNetUserLoginsCollection();
            if (loginUsers.Load(q))
            {
                loginUsers.MarkAllAsDeleted();
                loginUsers.Save();
            }

            return 1;
        }

        /// <summary>
        /// Inserts a new login in the UserLogins table
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, UserLoginInfo login)
        {
            AspNetUserLogins loginUser = new AspNetUserLogins();
            loginUser.LoginProvider = login.LoginProvider;
            loginUser.ProviderKey = login.ProviderKey;
            loginUser.UserId = user.Id;
            loginUser.Save();

            return 1;
        }

        /// <summary>
        /// Return a userId given a user's login
        /// </summary>
        /// <param name="userLogin">The user's login info</param>
        /// <returns></returns>
        public string FindUserIdByLogin(UserLoginInfo userLogin)
        {
            string userId = null;

            AspNetUserLoginsQuery q = new AspNetUserLoginsQuery();
            q.Select(q.UserId);
            q.Where(q.LoginProvider == userLogin.LoginProvider && q.ProviderKey == userLogin.ProviderKey);

            AspNetUserLogins login = new AspNetUserLogins();
            if(login.Load(q))
            {
                userId = login.UserId;
            }

            return userId;
        }

        /// <summary>
        /// Returns a list of user's logins
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId(string userId)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();

            AspNetUserLoginsQuery q = new AspNetUserLoginsQuery();
            q.Where(q.UserId == userId);

            AspNetUserLoginsCollection loginUsers = new AspNetUserLoginsCollection();
            if (loginUsers.Load(q))
            {
                foreach(AspNetUserLogins user in loginUsers)
                {
                    var login = new UserLoginInfo(user.LoginProvider, user.ProviderKey);
                    logins.Add(login);
                }
            }

            return logins;
        }
    }
}
