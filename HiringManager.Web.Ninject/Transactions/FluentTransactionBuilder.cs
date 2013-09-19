using System.Text;
using HiringManager.Transactions;
using Ninject;

namespace HiringManager.Web.Ninject.Transactions
{
    public class FluentTransactionBuilder : IFluentTransactionBuilder
    {
        private readonly IKernel _kernel;

        public IFluentTransactionRequestSyntax<TRequest> Receives<TRequest>()
        {
            return new FluentTransactionRequestSyntax<TRequest>(_kernel);
        }

        public FluentTransactionBuilder(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}
