using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput Map(TInput input);
    }
}
