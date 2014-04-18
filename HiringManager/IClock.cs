using System;

namespace HiringManager
{
    public interface IClock
    {
        DateTime Now { get;  }
        DateTime Today { get;  }
    }
}
