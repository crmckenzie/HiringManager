namespace HiringManager.DomainServices.Transactions
{
    public interface IUnitOfWork
    {
        IDbContext NewDbContext();
    }
}