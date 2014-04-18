using System.Data.Entity;
using HiringManager.EntityFramework;

namespace HiringManager.Web.Integration.Tests
{
    public class IntegrationTestDbConfiguration : DbConfiguration
    {
        public IntegrationTestDbConfiguration()
        {
            base.SetDatabaseInitializer(new DropCreateDatabaseAlways<HiringManagerDbContext>());
        }
    }
}