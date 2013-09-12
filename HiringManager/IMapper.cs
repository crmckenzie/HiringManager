using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public interface IMapper
    {
        TOutput Map<TInput, TOutput>(TInput input);
    }
}
