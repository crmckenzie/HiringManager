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



            return kernel;
        }

        private static void RebindExternalServices(this StandardKernel kernel)
        {
        }
    }
}