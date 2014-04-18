using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using HiringManager.EntityFramework;
using Configuration = HiringManager.EntityFramework.Migrations.Configuration;

namespace HiringManager.Web.App_Start
{
    public class DatabaseConfiguration : DbConfiguration
    {
        public static void Configure()
        {
            foreach (var connectionString in ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>())
            {
                var message = string.Format("Connection String Name: {0}; Value: {1}; Provider: {2};",
                    connectionString.Name, connectionString.ConnectionString, connectionString.ProviderName);
                Trace.WriteLine(message);
            }

            SetConfiguration(new DatabaseConfiguration());
            //new HiringManagerDbContext().Database.Initialize(force: false);
        }

        public DatabaseConfiguration()
        {
            var migrateDatabaseToLatestVersion = new MigrateDatabaseToLatestVersion<HiringManagerDbContext, Configuration>();
            base.SetDatabaseInitializer<HiringManagerDbContext>(null);

            var databaseLogFormatter = new DatabaseLogFormatter(row => Trace.WriteLine(row));
            base.AddInterceptor(databaseLogFormatter);
        }


    }


}