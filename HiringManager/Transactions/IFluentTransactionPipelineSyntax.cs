namespace HiringManager.Transactions
{
    public interface IFluentTransactionPipelineSyntax<TRequest, TResponse>
    {
        IFluentTransactionPipelineSyntax<TRequest, TResponse> WithAuthorization(params string[] roles);

        IFluentTransactionPipelineSyntax<TRequest, TResponse> WithRequestValidation(params string[] ruleSets);

        IFluentTransactionPipelineSyntax<TRequest, TResponse> WithPerformanceLogging();

        ITransaction<TRequest, TResponse> Build();

        ITransaction<TRequest, TResponse> Build<TTransaction>() where TTransaction : ITransaction<TRequest, TResponse>;
    }
}