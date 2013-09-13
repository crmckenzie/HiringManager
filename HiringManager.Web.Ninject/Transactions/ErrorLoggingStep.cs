using System;
using System.Diagnostics;
using HiringManager.Transactions;

namespace HiringManager.Web.Ninject.Transactions
{
    public class ErrorLoggingStep<TRequest, TResponse> : TransactionStepBase<TRequest, TResponse>
    {

        public ErrorLoggingStep(ITransaction<TRequest, TResponse> innerCommand)
            : base(innerCommand)
        {
        }

        public override TResponse Execute(TRequest request)
        {
            try
            {
                return InnerCommand.Execute(request);
            }
            catch (Exception e)
            {
                var msg = string.Format("{0}\n{1}", e.Message, e.StackTrace);
                Trace.TraceError(msg);
                throw;
            }
        }
    }
}