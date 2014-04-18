using System.Data.Entity;
using System.Diagnostics;
using HiringManager.EntityFramework;
using HiringManager.Web.Infrastructure.AutoMapper;
using Ninject;

namespace HiringManager.Web.Integration.Tests
{
    internal static class IntegrationTestConfiguration
    {
        public static IKernel Configure()
        {
            AutoMapperConfiguration.Configure();

            DbConfiguration.SetConfiguration(new IntegrationTestDbConfiguration());

            var kernel = new StandardKernel();
            new HiringManager.Web.Infrastructure.Ninject.NinjectConfiguration().Configure(kernel);

            kernel.RebindExternalServices();

            new NBuilderConfiguration()
                .IntegrationTestConfiguration()
                ;

            ResetDatabase();

            return kernel;
        }

        private static void ResetDatabase()
        {
            using (var context = new HiringManagerDbContext())
            {
                /*
                 * exec sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
                 * exec sp_MSforeachtable 'ALTER TABLE ? DISABLE TRIGGER ALL'
                 * 
                 * Now delete all the records found in all tables in your database by forcing cleanup through Delete or Truncate
                 * exec sp_MSforeachtable 'DELETE ?'
                 * 
                 * We have to enable the Constraints and Triggers back again:
                 * exec sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
                 * exec sp_MSforeachtable 'ALTER TABLE ? ENABLE TRIGGER ALL'
                 * 
                 * The final step is to reset the Identity column in all tables back to zero base index:
                 * exec sp_MSforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 BEGIN DBCC CHECKIDENT (''?'',RESEED,0) END'
                 */

                var db = context.Database;
                var statements = new[]
                                 {
                                     "exec sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'",
                                     "exec sp_MSforeachtable 'ALTER TABLE ? DISABLE TRIGGER ALL'",
                                     "exec sp_MSforeachtable 'DELETE ?'",
                                     "exec sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'",
                                     "exec sp_MSforeachtable 'ALTER TABLE ? ENABLE TRIGGER ALL'",
                                     "exec sp_MSforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 BEGIN DBCC CHECKIDENT (''?'',RESEED,0) END'",
                                 };

                foreach (var statement in statements)
                    db.ExecuteSqlCommand(statement);

            }

            //var nameOrConnectionString = typeof(HiringManagerDbContext).FullName;
            //if (!Database.Exists(nameOrConnectionString)) return;

            //Trace.WriteLine("Deleting database: " + nameOrConnectionString);
            //Database.Delete(nameOrConnectionString);

            //Trace.WriteLine("Creating database: " + nameOrConnectionString);
            //new HiringManagerDbContext().Database.Initialize(force: true);
        }


        private static void RebindExternalServices(this StandardKernel kernel)
        {
        }
    }
}