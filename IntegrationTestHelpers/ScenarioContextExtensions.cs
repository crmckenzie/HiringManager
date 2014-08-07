using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using TechTalk.SpecFlow;

namespace IntegrationTestHelpers
{
    public static class ScenarioContextExtensions
    {
        public static T GetFromNinject<T>(this ScenarioContext current)
        {
            var kernel = GetNinjectKernel(current);
            if (!current.ContainsKey(typeof(T).FullName))
            {
                var instance = kernel.Get<T>();
                current.Set<T>(instance, typeof(T).FullName);
            }

            var result = current.Get<T>(typeof(T).FullName);
            return result;
        }

        public static T GetNewInstanceFromNinject<T>(this ScenarioContext current)
        {
            return GetNinjectKernel(current).Get<T>();
        }

        public static IKernel GetNinjectKernel(this ScenarioContext current)
        {
            if (!current.ContainsKey("Ninject.Kernel"))
            {
                foreach (var connectionString in System.Configuration.ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>())
                {
                    var message = string.Format("Connection String Name: {0}; Value: {1}; Provider: {2};",
                        connectionString.Name, connectionString.ConnectionString, connectionString.ProviderName);
                    Trace.WriteLine(message);
                }

                var kernel = IntegrationTestConfiguration.Configure();
                current.Add("Ninject.Kernel", kernel);
            }
            var result = current["Ninject.Kernel"] as IKernel;
            return result;
        }



        public static T GetController<T>(this ScenarioContext context) where T : Controller
        {
            var controller = GetFromNinject<T>(context).Fake();
            return controller;
        }
    }
}
