namespace HiringManager.Transactions
{
    public interface IFluentTransactionRequestSyntax<in TRequest>
    {
        IFluentTransactionPipelineSyntax<TRequest, TResponse> Returns<TResponse>();
    }
}