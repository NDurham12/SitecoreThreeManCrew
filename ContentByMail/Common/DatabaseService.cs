using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore;

namespace ContentByMail.Common
{
    internal class DatabaseService
    {
        internal static Database ActiveDatabase
        {
            get
            {
                Database database = Context.ContentDatabase ?? Context.Database;
                if (database != null && database.Name != Constants.Databases.Core)
                    return database;
                return Factory.GetDatabase(Constants.Databases.Master.ToString());
            }
        }
        
    }
}
