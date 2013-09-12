namespace HiringManager
{
    public interface IFluentTransactionRequestSyntax<in TRequest>
    {
        IFluentTransactionDecoratorSyntax<TRequest, TResponse> Returns<TResponse>();
    }
}