namespace HiringManager
{
    public interface IFluentTransactionBuilder
    {
        IFluentTransactionRequestSyntax<TRequest> Receives<TRequest>();
    }
}