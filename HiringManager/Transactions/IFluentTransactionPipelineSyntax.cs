namespace HiringManager.Transactions
{
    public interface IFluentTransactionPipelineSyntax<in TRequest, out TResponse>
    {
        IFluentTransactionPipelineSyntax<TRequest, TResponse> WithAuthorization(params string[] roles);

        IFluentTransactionPipelineSyntax<TRequest, TResponse> WithRequestValidation(params string[] ruleSets);

        IFluentTransactionPipelineSyntax<TRequest, TResponse> WithPerformanceLogging();

        ITransaction<TRequest, TResponse> Build();
    }
}