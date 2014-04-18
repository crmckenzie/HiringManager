using System;
using System.Linq;
using System.Security.Principal;
using HiringManager.EntityModel;

namespace HiringManager.DomainServices.Impl
{
    public class UserSession :IUserSession
    {
        private readonly IPrincipal _principal;
        private readonly IDbContext _dbContext;
        public int? ManagerId { get { return _manager.Value.ManagerId; }}
        public string UserName { get { return _manager.Value.UserName; } }
        public string DisplayName { get { return _manager.Value.Name;  } }

        private readonly Lazy<Manager> _manager;

        private Manager GetOrCreateManager()
        {
            var result = _dbContext.Query<Manager>().SingleOrDefault(row => row.UserName == _principal.Identity.Name);

            if (result == null)
            {
                result = new Manager()
                         {
                             Name = _principal.Identity.Name,
                             UserName = _principal.Identity.Name,
                         };

                _dbContext.Add(result);
                _dbContext.SaveChanges();
            }
            
            return result;
        }        

        public UserSession(IPrincipal principal, IDbContext dbContext)
        {
            _principal = principal;
            _dbContext = dbContext;

            this._manager = new Lazy<Manager>(GetOrCreateManager);
        }
    }
}
