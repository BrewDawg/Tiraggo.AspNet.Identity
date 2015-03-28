using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Tiraggo.AspNet.Identity
{
    public static class IIdentityExtensionMethods
    {
        public static List<string> Roles(this IIdentity identity)
        {
            List<string> roles = null;

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                roles = claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role)
                               .Select(c => c.Value)
                               .ToList();
            }
            else
            {
                roles = new List<string>();
            }

            return roles;
        }

        public static string SecurityStamp(this IIdentity identity)
        {
            string securityStamp = null;

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                securityStamp = claimsIdentity.Claims
                    .Where(c => c.Type == "AspNet.Identity.SecurityStamp")
                    .Select(c => c.Value).SingleOrDefault();
            }

            return securityStamp;
        }

        public static string UserId(this IIdentity identity)
        {
            string securityStamp = null;

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                securityStamp = claimsIdentity.Claims
                    .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    .Select(c => c.Value).SingleOrDefault();
            }

            return securityStamp;
        }

        public static string UserName(this IIdentity identity)
        {
            string securityStamp = null;

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                securityStamp = claimsIdentity.Claims
                    .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    .Select(c => c.Value).SingleOrDefault();
            }

            return securityStamp;
        }
    }
}
