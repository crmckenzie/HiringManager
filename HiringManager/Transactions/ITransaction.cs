namespace HiringManager.Transactions
{
    public interface ITransaction<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
