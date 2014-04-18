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
            var nameOrConnectionString = typeof(HiringManagerDbContext).FullName;
            if (!Database.Exists(nameOrConnectionString)) return;

            Trace.WriteLine("Deleting database: " + nameOrConnectionString);
            Database.Delete(nameOrConnectionString);

            Trace.WriteLine("Creating database: " + nameOrConnectionString);
            new HiringManagerDbContext().Database.Initialize(force: true);
        }


        private static void RebindExternalServices(this StandardKernel kernel)
        {
        }
    }
}