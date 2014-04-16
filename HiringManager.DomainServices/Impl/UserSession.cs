using System;
using System.Linq;
using System.Security.Principal;
using HiringManager.EntityModel;

namespace HiringManager.DomainServices.Impl
{
    public class UserSession :IUserSession
    {
        private readonly IPrincipal _principal;
        private readonly IRepository _repository;
        public int? ManagerId { get { return _manager.Value.ManagerId; }}
        public string UserName { get { return _manager.Value.UserName; } }
        public string DisplayName { get { return _manager.Value.Name;  } }

        private readonly Lazy<Manager> _manager;

        private Manager GetOrCreateManager()
        {
            var result = _repository.Query<Manager>().SingleOrDefault(row => row.UserName == _principal.Identity.Name);

            if (result == null)
            {
                result = new Manager()
                         {
                             Name = _principal.Identity.Name,
                             UserName = _principal.Identity.Name,
                         };

                _repository.Store(result);
                _repository.Commit();
            }
            
            return result;
        }        

        public UserSession(IPrincipal principal, IRepository repository)
        {
            _principal = principal;
            _repository = repository;

            this._manager = new Lazy<Manager>(GetOrCreateManager);
        }
    }
}
