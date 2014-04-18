namespace HiringManager.DomainServices.Impl
{
    public interface IUnitOfWork
    {
        IDbContext NewDbContext();
    }
}