﻿using BusinessObjects;
using System.Collections.Generic;
using System.Security.Claims;

namespace Tiraggo.AspNet.Identity
{
    /// <summary>
    /// Class that represents the UserClaims table 
    /// </summary>
    public class UserClaimsTable : IdentityBaseTable
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="database"></param>
        public UserClaimsTable(string connectionName = null)
        {
            ConnectionName = connectionName;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            ConnectionService.ThreadVanityUrl = ConnectionName;

            ClaimsIdentity claims = new ClaimsIdentity();

            AspNetUserClaimsQuery q = new AspNetUserClaimsQuery();
            q.Where(q.UserId == userId);

            AspNetUserClaimsCollection coll = new AspNetUserClaimsCollection();
            SetConnection(coll);
            if (coll.Load(q))
            {
                foreach (AspNetUserClaims c in coll)
                {
                    Claim claim = new Claim(c.ClaimType, c.ClaimValue);
                    claims.AddClaim(claim);
                }
            }

            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            ConnectionService.ThreadVanityUrl = ConnectionName;

            AspNetUserClaimsQuery q = new AspNetUserClaimsQuery();
            q.Where(q.UserId == userId);

            AspNetUserClaimsCollection coll = new AspNetUserClaimsCollection();
            SetConnection(coll);
            if (coll.Load(q))
            {
                coll.MarkAllAsDeleted();
                coll.Save();
            }

            return 1;
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public int Insert(Claim userClaim, string userId)
        {
            ConnectionService.ThreadVanityUrl = ConnectionName;

            AspNetUserClaims claim = new AspNetUserClaims();
            SetConnection(claim);
            claim.ClaimType = userClaim.Type;
            claim.ClaimValue = userClaim.Value;
            claim.UserId = userId;
            claim.Save();

            return 1;
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, Claim claim)
        {
            ConnectionService.ThreadVanityUrl = ConnectionName;

            AspNetUserClaimsQuery q = new AspNetUserClaimsQuery();
            q.Where(q.UserId == user.Id && q.ClaimType == claim.Type && q.ClaimValue == claim.Value);

            AspNetUserClaimsCollection coll = new AspNetUserClaimsCollection();
            SetConnection(coll);
            if (coll.Load(q))
            {
                coll.MarkAllAsDeleted();
                coll.Save();
            }

            return 1;
        }
    }
}
