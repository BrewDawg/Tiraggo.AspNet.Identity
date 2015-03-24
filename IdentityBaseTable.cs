using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiraggo.Core;

namespace Tiraggo.AspNet.Identity
{
    public class IdentityBaseTable
    {
        private string _connectionName;

        public string ConnectionName
        {
            get { return _connectionName; }
            set { _connectionName = value; }
        }

        protected void SetConnection(tgEntity entity)
        {
            if(!String.IsNullOrWhiteSpace(_connectionName))
            {
                entity.ConnectionServiceOverride(_connectionName);
            }
        }

        protected void SetConnection(tgEntityCollectionBase coll)
        {
            if (!String.IsNullOrWhiteSpace(_connectionName))
            {
                coll.ConnectionServiceOverride(_connectionName);
            }
        }
    }
}
