using Microsoft.AspNet.Identity.EntityFramework;

namespace HiringManager.Web.Models.Accounts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}