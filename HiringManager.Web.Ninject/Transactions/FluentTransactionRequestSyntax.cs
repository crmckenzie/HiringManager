using HiringManager.Transactions;
using Ninject;

namespace HiringManager.Web.Ninject.Transactions
{
    public class FluentTransactionRequestSyntax<TRequest> : IFluentTransactionRequestSyntax<TRequest>
    {
        private readonly IKernel _kernel;

        public IFluentTransactionPipelineSyntax<TRequest, TResponse> Returns<TResponse>()
        {
            return new FluentTransactionPipelineSyntax<TRequest, TResponse>(_kernel);
        }

        public FluentTransactionRequestSyntax(IKernel kernel)
        {
            _kernel = kernel;
        }


    }
}