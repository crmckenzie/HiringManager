using Microsoft.AspNet.Identity.EntityFramework;

namespace HiringManager.Web.Models.Accounts
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Title { get; set; }
        public string DisplayName { get; set; }
    }
}