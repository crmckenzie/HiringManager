using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using HiringManager.Domain;

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

        private Manager GetManager()
        {
            return _repository.Query<Manager>().Single(row => row.UserName == _principal.Identity.Name);
        }        

        public UserSession(IPrincipal principal, IRepository repository)
        {
            _principal = principal;
            _repository = repository;

            this._manager = new Lazy<Manager>(GetManager);
        }
    }
}
