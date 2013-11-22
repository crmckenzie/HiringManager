using HiringManager.DomainServices;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HiringManager.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}