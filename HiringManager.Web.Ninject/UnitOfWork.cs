using HiringManager.DomainServices.Impl;
using Ninject;

namespace HiringManager.Web.Ninject
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IKernel _kernel;

        public IRepository NewRepository()
        {
            return _kernel.Get<IRepository>();
        }

        public UnitOfWork(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}