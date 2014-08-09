using HiringManager.DomainServices;
using HiringManager.DomainServices.Transactions;
using Ninject;

namespace HiringManager.Web.Ninject
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IKernel _kernel;

        public IDbContext NewDbContext()
        {
            return _kernel.Get<IDbContext>();
        }

        public UnitOfWork(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}