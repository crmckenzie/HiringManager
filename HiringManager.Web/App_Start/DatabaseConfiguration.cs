using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using HiringManager.EntityFramework;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Accounts;
using Configuration = HiringManager.EntityFramework.Migrations.Configuration;

namespace HiringManager.Web.App_Start
{
    public static class DatabaseConfiguration
    {
        public static void Configure()
        {
            try
            {
                foreach (var connectionString in ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>()
                    )
                {
                    var message = string.Format("Connection String Name: {0}; Value: {1}; Provider: {2};",
                        connectionString.Name, connectionString.ConnectionString, connectionString.ProviderName);
                    Trace.WriteLine(message);

                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<Repository, Configuration>());


                    new Repository().Database.Initialize(force: false);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}