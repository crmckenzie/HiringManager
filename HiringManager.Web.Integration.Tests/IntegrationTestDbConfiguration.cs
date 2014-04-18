using System.Data.Entity;
using System.Data.Entity.Migrations.Infrastructure;
using HiringManager.EntityFramework;

namespace HiringManager.Web.Integration.Tests
{
    public class IntegrationTestDbConfiguration : DbConfiguration
    {
        public IntegrationTestDbConfiguration()
        {
            base.SetDatabaseInitializer<HiringManagerDbContext>(null);
        }
    }
}