using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace HiringManager.DomainServices.Authentication
{
    public interface IUserManager : IDisposable
    {
        Task<ApplicationUser> FindAsync(UserLoginInfo userLogin);
        Task<ApplicationUser> FindAsync(string userName, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user);

        Task<IdentityResult> RemoveLoginAsync(string getUserId, UserLoginInfo userLoginInfo);
        Task<IdentityResult> ChangePasswordAsync(string getUserId, string oldPassword, string newPassword);
        Task<IdentityResult> AddPasswordAsync(string getUserId, string newPassword);
        Task<IdentityResult> AddLoginAsync(string getUserId, UserLoginInfo login);
        ICollection<UserLoginInfo> GetLogins(string getUserId);
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string applicationCookie);
        ApplicationUser FindById(string getUserId);
    }
}