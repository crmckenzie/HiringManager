using HiringManager.Mappers;
using Ninject;

namespace HiringManager.Web.Ninject.Mappers
{
    public class FluentMapper : IFluentMapper
    {
        private readonly IKernel _kernel;

        public IFluentMapperSyntax<TOutput> Map<TOutput>()
        {
            return new FluentMapperSyntax<TOutput>(_kernel);
        }

        public IEnumerableFluentMapperSyntax<TOutput> MapEnumerable<TOutput>()
        {
            return new EnumerableFluentMapperSyntax<TOutput>(_kernel);
        }

        public FluentMapper(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}