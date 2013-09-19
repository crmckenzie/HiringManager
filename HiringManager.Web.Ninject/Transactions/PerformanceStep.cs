using System.Diagnostics;
using HiringManager.Transactions;

namespace HiringManager.Web.Ninject.Transactions
{
    public class PerformanceStep<TRequest, TResponse> : TransactionStepBase<TRequest, TResponse>
    {
        private readonly IClock _clock;

        public PerformanceStep(ITransaction<TRequest, TResponse> innerCommand, IClock clock)
            : base(innerCommand)
        {
            _clock = clock;
        }

        public override TResponse Execute(TRequest request)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            try
            {
                var startTime = _clock.Now;

                var result = base.InnerCommand.Execute(request);

                var endTime = _clock.Now;

                if (typeof(TResponse).Implements<IPerformanceResponse>())
                {
                    var response = result as IPerformanceResponse;
                    response.StartTime = startTime;
                    response.EndTime = endTime;
                }

                return result;
            }
            finally
            {
                stopwatch.Stop();
                var innerType = base.GetInnermostCommandType();
                Trace.TraceInformation("Executed {0} in {1:N} milliseconds.", innerType.FullName, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}