using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using HiringManager.EntityFramework;

namespace HiringManager.Web.Integration.Tests
{
    public class IntegrationTestDbConfiguration : DbConfiguration
    {



        public IntegrationTestDbConfiguration()
        {
            base.SetDatabaseInitializer(new DropCreateDatabaseAlways<Repository>());

            var databaseLogFormatter = new DatabaseLogFormatter(message => Trace.WriteLine(message));
            base.AddInterceptor(databaseLogFormatter);


        }
    }
}