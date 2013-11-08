using System.Data.Entity;
using HiringManager.EntityFramework;
using HiringManager.Web.App_Start;
using Ninject;
using TechTalk.SpecFlow;

namespace HiringManager.Web.Integration.Tests
{
    internal static class ScenarioContextExtensions
    {
        public static T GetFromNinject<T>(this ScenarioContext current)
        {
            return current.GetNinjectKernel().Get<T>();
        }

        public static IKernel GetNinjectKernel(this ScenarioContext current)
        {
            if (!current.ContainsKey("Ninject.Kernel"))
            {
                //var nameOrConnectionString = typeof(Repository).FullName;
                //if (Database.Exists(nameOrConnectionString))
                
                //    Database.Delete(nameOrConnectionString);

                DatabaseConfiguration.Configure();

                //Database.SetInitializer(new DropCreateDatabaseAlways<Repository>());
                //new Repository().Database.Initialize(force: true);


                var kernel = IntegrationTestConfiguration.Configure();
                current.Add("Ninject.Kernel", kernel);
            }
            var result = current["Ninject.Kernel"] as IKernel;
            return result;
        }
    }
}
