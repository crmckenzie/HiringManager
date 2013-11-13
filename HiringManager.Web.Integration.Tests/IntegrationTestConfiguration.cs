using System.Data.Entity;
using System.Diagnostics;
using HiringManager.EntityFramework;
using Ninject;

namespace HiringManager.Web.Integration.Tests
{
    internal static class IntegrationTestConfiguration
    {
        public static IKernel Configure()
        {
            var kernel = new StandardKernel();
            new HiringManager.Web.Infrastructure.Ninject.NinjectConfiguration().Configure(kernel);

            kernel.RebindExternalServices();

            new NBuilderConfiguration()
                .IntegrationTestConfiguration()
                ;

            DbConfiguration.SetConfiguration(new IntegrationTestDbConfiguration());
            ResetDatabase();

            return kernel;
        }

        private static void ResetDatabase()
        {
            var nameOrConnectionString = typeof (Repository).FullName;
            if (!Database.Exists(nameOrConnectionString)) return;

            Trace.WriteLine("Deleting database: " + nameOrConnectionString);
            Database.Delete(nameOrConnectionString);

            Trace.WriteLine("Creating database: " + nameOrConnectionString);
            new Repository().Database.Initialize(force: true);
        }


        private static void RebindExternalServices(this StandardKernel kernel)
        {
        }
    }
}