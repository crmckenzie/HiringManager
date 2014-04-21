using System.Data.Entity;
using HiringManager.EntityFramework;

namespace IntegrationTestHelpers
{
    public class IntegrationTestDbConfiguration : DbConfiguration
    {
        public IntegrationTestDbConfiguration()
        {
            base.SetDatabaseInitializer<HiringManagerDbContext>(null);
        }
    }
}