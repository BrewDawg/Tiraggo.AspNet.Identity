using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Tiraggo.Core;
using Tiraggo.DynamicQuery;
using Tiraggo.Interfaces;

namespace Tiraggo.AspNet.Identity
{
    class SchemaCreator
    {
        public void CreateSchema(string connectionName = null)
        {
            string script = GetEmbeddedResourceFile("Tiraggo.AspNet.Identity.Schema.sql");

            // split script on GO command
            IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            using (tgTransactionScope scope = new tgTransactionScope())
            {
                tgUtility util = new tgUtility();
                if (connectionName != null)
                {
                    util.ConnectionServiceOverride(connectionName);
                }

                foreach (string commandString in commandStrings)
                {
                    if (!String.IsNullOrWhiteSpace(commandString))
                    {
                        switch (commandString)
                        {
                            case "SET ANSI_NULLS ON":
                            case "SET QUOTED_IDENTIFIER ON":
                                continue;

                            default:
                                util.ExecuteNonQuery(tgQueryType.Text, commandString);
                                break;
                        }
                    }
                }

                scope.Complete();
            }
        }
        private string GetEmbeddedResourceFile(string resourceName)
        {
            string file = "";

            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                file = reader.ReadToEnd();
            }

            return file;
        }
    }
}
