using System;

namespace HiringManager
{
    public class Clock : IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
        public DateTime Today { get { return DateTime.Today; } }
    }
}