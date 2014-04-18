using System.Data.Entity;

namespace HiringManager.EntityFramework
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }
    }
}