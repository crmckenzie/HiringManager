using System.Data.Entity.Migrations;
using HiringManager.EntityFramework;

namespace HiringManager.Web.Integration.Tests
{
    internal class IntegrationTestMigrationsConfiguration : DbMigrationsConfiguration<Repository>
    {
        public IntegrationTestMigrationsConfiguration()
        {
            base.AutomaticMigrationsEnabled = false;
        }
    }
}