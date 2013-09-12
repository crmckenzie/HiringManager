namespace HiringManager
{
    public interface IFluentTransactionDecoratorSyntax<in TRequest, out TResponse>
    {
        IFluentTransactionDecoratorSyntax<TRequest, TResponse> WithAuthorization(params string[] roles);

        IFluentTransactionDecoratorSyntax<TRequest, TResponse> WithRequestValidation(params string[] ruleSets);

        IFluentTransactionDecoratorSyntax<TRequest, TResponse> WithPerformanceLogging();

        ITransaction<TRequest, TResponse> Build();
    }
}