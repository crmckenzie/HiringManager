using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace HiringManager.DomainServices
{
    public static class IdentityExtensions
    {
        public static string GetDisplayName(this IIdentity user)
        {
            var claimsIdentity = user as ClaimsIdentity;
            if (claimsIdentity != null)
                return claimsIdentity.Label;

            return user.GetUserName();
        }
    }
}