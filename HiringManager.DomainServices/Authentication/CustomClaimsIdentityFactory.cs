using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace HiringManager.DomainServices.Authentication
{
    public class CustomClaimsIdentityFactory : ClaimsIdentityFactory<ApplicationUser>
    {
        public override async Task<ClaimsIdentity> CreateAsync(UserManager<ApplicationUser> manager,
            ApplicationUser user, string authenticationType)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if (user == null)
                throw new ArgumentNullException("user");

            var id = new ClaimsIdentity(authenticationType, this.UserNameClaimType, this.RoleClaimType);
            id.AddClaim(new Claim(this.UserIdClaimType, user.Id, "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(this.UserNameClaimType, user.UserName, "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            if (manager.SupportsUserRole)
            {
                var roles = await manager.GetRolesAsync(user.Id);
                foreach (var str in roles)
                    id.AddClaim(new Claim(this.RoleClaimType, str, "http://www.w3.org/2001/XMLSchema#string"));
            }
            if (manager.SupportsUserClaim)
                id.AddClaims(await manager.GetClaimsAsync(user.Id));

            id.AddClaim(new Claim(Claims.DisplayNameKey, user.DisplayName));

            return id;
        }
    }
}