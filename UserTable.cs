using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Tiraggo.AspNet.Identity
{
    /// <summary>
    /// Class that represents the Users table
    /// </summary>
    public class UserTable<TUser> : IdentityBaseTable where TUser :IdentityUser
    {
        /// <summary>
        /// Constructor that takes an optional connection name 
        /// </summary>
        /// <param name="database"></param>
        public UserTable(string connectionName = null)
        {
            ConnectionName = connectionName;
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            string userName = null;

            AspNetUsers user = new AspNetUsers();
            SetConnection(user);
            if(user.LoadByPrimaryKey(userId))
            {
                userName = user.UserName;
            }

            return userName;
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            string userId = null;

            AspNetUsersQuery q = new AspNetUsersQuery();
            q.Select(q.Id);
            q.Where(q.UserName == userName);

            AspNetUsers user = new AspNetUsers();
            SetConnection(user);
            if(user.Load(q))
            {
                userId = user.Id;
            }

            return userId;
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public TUser GetUserById(string userId)
        {
            TUser newUser = null;

            AspNetUsersQuery q = new AspNetUsersQuery();
            q.Where(q.Id == userId);

            AspNetUsers user = new AspNetUsers();
            SetConnection(user);
            if (user.Load(q))
            {
                newUser = (TUser)Activator.CreateInstance(typeof(TUser));

                newUser.Id = user.Id;
                newUser.UserName = user.UserName;
                newUser.PasswordHash = user.PasswordHash;
                newUser.SecurityStamp = user.SecurityStamp;
                newUser.Email = user.Email;
                newUser.EmailConfirmed = user.EmailConfirmed == null ? false : user.EmailConfirmed.Value;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed == null ? false : user.PhoneNumberConfirmed.Value;
                newUser.LockoutEnabled = user.LockoutEnabled == null ? false : user.LockoutEnabled.Value;
                newUser.LockoutEndDateUtc = user.LockoutEndDateUtc == null ? DateTime.UtcNow : user.LockoutEndDateUtc.Value;
                newUser.AccessFailedCount = user.AccessFailedCount == null ? 0 : user.AccessFailedCount.Value;
            }

            return newUser;
        }

        /// <summary>
        /// Returns a list of TUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string userName)
        {
            List<TUser> users = new List<TUser>();

            AspNetUsersQuery q = new AspNetUsersQuery();
            q.Where(q.UserName == userName);

            int id = Thread.CurrentThread.ManagedThreadId;

            AspNetUsersCollection coll = new AspNetUsersCollection();
            SetConnection(coll);
            if (coll.Load(q))
            {
                foreach (AspNetUsers user in coll)
                {
                    TUser newUser = (TUser)Activator.CreateInstance(typeof(TUser));

                    newUser.Id = user.Id;
                    newUser.UserName = user.UserName;
                    newUser.PasswordHash = user.PasswordHash;
                    newUser.SecurityStamp = user.SecurityStamp;
                    newUser.Email = user.Email;
                    newUser.EmailConfirmed = user.EmailConfirmed == null ? false : user.EmailConfirmed.Value;
                    newUser.PhoneNumber = user.PhoneNumber;
                    newUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed == null ? false : user.PhoneNumberConfirmed.Value;
                    newUser.LockoutEnabled = user.LockoutEnabled == null ? false : user.LockoutEnabled.Value;
                    newUser.LockoutEndDateUtc = user.LockoutEndDateUtc == null ? DateTime.UtcNow : user.LockoutEndDateUtc.Value;
                    newUser.AccessFailedCount = user.AccessFailedCount == null ? 0 : user.AccessFailedCount.Value;

                    users.Add(newUser);
                }
            }

            return users;
        }

        public List<TUser> GetUserByEmail(string email)
        {
            List<TUser> users = new List<TUser>();

            AspNetUsersQuery q = new AspNetUsersQuery();
            q.Where(q.Email == email);

            AspNetUsersCollection coll = new AspNetUsersCollection();
            SetConnection(coll);
            if (coll.Load(q))
            {
                foreach (AspNetUsers user in coll)
                {
                    TUser newUser = (TUser)Activator.CreateInstance(typeof(TUser));

                    newUser.Id = user.Id;
                    newUser.UserName = user.UserName;
                    newUser.PasswordHash = user.PasswordHash;
                    newUser.SecurityStamp = user.SecurityStamp;
                    newUser.Email = user.Email;
                    newUser.EmailConfirmed = user.EmailConfirmed == null ? false : user.EmailConfirmed.Value;
                    newUser.PhoneNumber = user.PhoneNumber;
                    newUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed == null ? false : user.PhoneNumberConfirmed.Value;
                    newUser.LockoutEnabled = user.LockoutEnabled == null ? false : user.LockoutEnabled.Value;
                    newUser.LockoutEndDateUtc = user.LockoutEndDateUtc == null ? DateTime.UtcNow : user.LockoutEndDateUtc.Value;
                    newUser.AccessFailedCount = user.AccessFailedCount == null ? 0 : user.AccessFailedCount.Value;
                    newUser.TwoFactorEnabled = user.TwoFactorEnabled == null ? false : user.TwoFactorEnabled.Value;

                    users.Add(newUser);
                }
            }

            return users;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            string passwordHash = null;

            AspNetUsersQuery q = new AspNetUsersQuery();
            q.Select(q.PasswordHash);
            q.Where(q.Id == userId);

            AspNetUsers user = new AspNetUsers();
            SetConnection(user);
            if(user.Load(q))
            {
                passwordHash = user.PasswordHash;
            }

            return passwordHash;
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public int SetPasswordHash(string userId, string passwordHash)
        {
            AspNetUsers user = new AspNetUsers();
            SetConnection(user);
            if(user.LoadByPrimaryKey(userId))
            {
                user.PasswordHash = passwordHash;
                user.Save();
            }

            return 1;
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(string userId)
        {
            string stamp = null;

            AspNetUsersQuery q = new AspNetUsersQuery();
            q.Select(q.SecurityStamp);
            q.Where(q.Id == userId);

            AspNetUsers user = new AspNetUsers();
            SetConnection(user);
            if (user.Load(q))
            {
                stamp = user.SecurityStamp;
            }

            return stamp;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(TUser user)
        {
            AspNetUsers newUser = new AspNetUsers();
            SetConnection(newUser);

            newUser.UserName = user.UserName;
            newUser.Id = user.Id;
            newUser.PasswordHash = user.PasswordHash;
            newUser.SecurityStamp = user.SecurityStamp;
            newUser.Email = user.Email;
            newUser.EmailConfirmed = user.EmailConfirmed;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            newUser.AccessFailedCount = user.AccessFailedCount;
            newUser.LockoutEnabled = user.LockoutEnabled;
            newUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
            newUser.TwoFactorEnabled = user.TwoFactorEnabled;

            newUser.Save();

            return 1;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private int Delete(string userId)
        {
            try
            {
                AspNetUsers user = new AspNetUsers();
                SetConnection(user);
                user.Id = userId;
                user.AcceptChanges();
                user.MarkAsDeleted();
                user.Save();
            }
            catch { }

            return 1;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(TUser user)
        {
            try
            {
                Delete(user.Id);
            }
            catch { }

            return 1;
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(TUser user)
        {
            AspNetUsers newUser = new AspNetUsers();
            SetConnection(newUser);
            if (newUser.LoadByPrimaryKey(user.Id))
            {
                newUser.Id = user.Id;
                newUser.UserName = user.UserName;
                newUser.PasswordHash = user.PasswordHash;
                newUser.SecurityStamp = user.SecurityStamp;
                newUser.Email = user.Email;
                newUser.EmailConfirmed = user.EmailConfirmed;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                newUser.LockoutEnabled = user.LockoutEnabled;
                newUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
                newUser.AccessFailedCount = user.AccessFailedCount;
                newUser.TwoFactorEnabled = user.TwoFactorEnabled;

                newUser.Save();
            }

            return 1;
        }
    }
}
