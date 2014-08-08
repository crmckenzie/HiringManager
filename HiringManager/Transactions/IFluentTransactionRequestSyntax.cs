namespace HiringManager.Transactions
{
    public interface IFluentTransactionRequestSyntax<TRequest>
    {
        IFluentTransactionPipelineSyntax<TRequest, TResponse> Returns<TResponse>();
    }
}