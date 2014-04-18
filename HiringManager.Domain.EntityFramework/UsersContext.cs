using System.Data.Entity;

namespace HiringManager.EntityFramework
{
    public class UsersContext : System.Data.Entity.DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }
    }
}