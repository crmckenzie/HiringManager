using System.Collections.Generic;
using HiringManager.Mappers;
using Ninject;

namespace HiringManager.Web.Ninject.Mappers
{
    public class FluentMapperSyntax<TOutput> : IFluentMapperSyntax<TOutput>
    {
        private readonly IKernel _kernel;

        public TOutput From<TInput>(TInput input)
        {
            var converter = _kernel.Get<IMapper<TInput, TOutput>>();
            var result = converter.Map(input);
            return result;
        }

        public TOutput FromEnumerable<TInput>(IEnumerable<TInput> input)
        {
            var converter = _kernel.Get<IMapper<IEnumerable<TInput>, TOutput>>();
            var result = converter.Map(input);
            return result;
        }

        public FluentMapperSyntax(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}