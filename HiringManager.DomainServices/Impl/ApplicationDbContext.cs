using Microsoft.AspNet.Identity.EntityFramework;

namespace HiringManager.DomainServices.Impl
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}