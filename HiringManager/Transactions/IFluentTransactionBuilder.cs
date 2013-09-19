namespace HiringManager.Transactions
{
    public interface IFluentTransactionBuilder
    {
        IFluentTransactionRequestSyntax<TRequest> Receives<TRequest>();
    }

}