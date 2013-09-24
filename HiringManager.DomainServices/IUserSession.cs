using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager.DomainServices
{
    public interface IUserSession
    {
        int? ManagerId { get; }
        string UserName { get; }
        string DisplayName { get;  }
    }
}
