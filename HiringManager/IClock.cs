using System;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public interface IClock
    {
        DateTime Now { get;  }
        DateTime Today { get;  }
    }
}
