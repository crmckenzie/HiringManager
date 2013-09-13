using System;

namespace HiringManager.Web.Ninject.Transactions
{
    public interface IPerformanceResponse
    {
        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }
    }
}