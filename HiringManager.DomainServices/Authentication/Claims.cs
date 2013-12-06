using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace HiringManager.DomainServices.Authentication
{

    public static class Claims
    {
        public const string DisplayNameKey = "DisplayName";

        public static string TryGetClaim(this ClaimsIdentity claimsIdentity, string type)
        {
            if (claimsIdentity.HasClaim(p => p.Type == type))
                return claimsIdentity.FindFirst(r => r.Type == type).Value;
            return null;
        }

        public static string GetDisplayName(this IIdentity user)
        {
            var claimsIdentity = user as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                if (!string.IsNullOrWhiteSpace(claimsIdentity.Label))
                    return claimsIdentity.Label;

                var displayName = claimsIdentity.TryGetClaim(DisplayNameKey);
                if (!string.IsNullOrWhiteSpace(displayName))
                    return displayName;
            }

            return user.GetUserName();
        }
    }
}