using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HiringManager.DomainServices.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HiringManager.DomainServices.Impl
{

    public class UserManager : IUserManager, System.IDisposable
    {
        private readonly UserManager<ApplicationUser> _db;

        public UserManager(UserManager<ApplicationUser> db)
        {
            this._db = db;
            _db.ClaimsIdentityFactory = new CustomClaimsIdentityFactory();
        }

        public Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return _db.FindAsync(userName, password);
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo userLogin)
        {
            return _db.FindAsync(userLogin);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _db.CreateAsync(user, password);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return _db.CreateAsync(user);
        }

        public Task<IdentityResult> RemoveLoginAsync(string getUserId, UserLoginInfo userLoginInfo)
        {
            return _db.RemoveLoginAsync(getUserId, userLoginInfo);
        }

        public Task<IdentityResult> ChangePasswordAsync(string getUserId, string oldPassword, string newPassword)
        {
            return _db.ChangePasswordAsync(getUserId, oldPassword, newPassword);
        }

        public Task<IdentityResult> AddPasswordAsync(string getUserId, string newPassword)
        {
            return _db.AddPasswordAsync(getUserId, newPassword);
        }

        public Task<IdentityResult> AddLoginAsync(string getUserId, UserLoginInfo login)
        {
            return _db.AddLoginAsync(getUserId, login);
        }

        public ICollection<UserLoginInfo> GetLogins(string getUserId)
        {
            return _db.GetLogins(getUserId);
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string applicationCookie)
        {
            return _db.CreateIdentityAsync(user, applicationCookie);
        
        }

        public ApplicationUser FindById(string getUserId)
        {
            return _db.FindById(getUserId);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}