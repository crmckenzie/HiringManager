using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager.Web.ApplicationServices
{
    public interface IAccountService
    {
        int? Register(RegisterModel model);
    }
}
