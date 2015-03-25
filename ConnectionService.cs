using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiraggo.Interfaces;

namespace Tiraggo.AspNet.Identity
{
    public class ConnectionService : IConnectionNameService
    {
        [ThreadStatic]
        private static string _threadVanityUrl = null;

        public static string ThreadVanityUrl
        {
            get
            {
                return _threadVanityUrl;
            }

            set
            {
                _threadVanityUrl = value;
            }
        }

        string IConnectionNameService.GetName()
        {
            return _threadVanityUrl;
        }
    }
}
