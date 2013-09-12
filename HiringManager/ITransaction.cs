using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public interface ITransaction<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
