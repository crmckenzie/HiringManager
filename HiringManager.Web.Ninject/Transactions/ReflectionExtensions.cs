using System;
using System.Linq;

namespace HiringManager.Web.Ninject.Transactions
{
    public static class ReflectionExtensions
    {
        public static bool Implements<TSuperType>(this Type self)
        {
            return self.GetInterfaces().Contains(typeof(TSuperType));
        }

    }
}