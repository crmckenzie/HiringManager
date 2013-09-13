using System;
using HiringManager.Transactions;

namespace HiringManager.Web.Ninject.Transactions
{
    public abstract class TransactionStepBase<TRequest, TResponse> : ITransaction<TRequest, TResponse>
    {
        protected ITransaction<TRequest, TResponse> InnerCommand { get; private set; }

        protected TransactionStepBase(ITransaction<TRequest, TResponse> innerCommand)
        {
            InnerCommand = innerCommand;
        }

        public Type GetInnermostCommandType()
        {
            var innerDecorator = InnerCommand as TransactionStepBase<TRequest, TResponse>;
            if (innerDecorator != null)
            {
                return innerDecorator.GetInnermostCommandType();
            }
            return InnerCommand.GetType();
        }

        public abstract TResponse Execute(TRequest request);

    }
}