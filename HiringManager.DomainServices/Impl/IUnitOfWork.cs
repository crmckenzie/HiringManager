namespace HiringManager.DomainServices.Impl
{
    public interface IUnitOfWork
    {
        IRepository NewRepository();
    }
}