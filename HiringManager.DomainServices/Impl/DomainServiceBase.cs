using HiringManager.Transactions;

namespace HiringManager.DomainServices.Impl
{
    public class DomainServiceBase
    {
        protected readonly IFluentTransactionBuilder _builder;

        public DomainServiceBase(IFluentTransactionBuilder builder)
        {
            _builder = builder;
        }

        protected TResponse Execute<TRequest, TResponse>(TRequest request)
        {
            var transaction = _builder
                .Receives<TRequest>()
                .Returns<TResponse>()
                .WithRequestValidation()
                .WithPerformanceLogging()
                .Build()
                ;

            var response = transaction.Execute(request);

            return response;

        }
    }
}