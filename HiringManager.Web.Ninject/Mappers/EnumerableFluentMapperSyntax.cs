using System.Collections.Generic;
using System.Linq;
using HiringManager.Mappers;
using Ninject;

namespace HiringManager.Web.Ninject.Mappers
{
    public class EnumerableFluentMapperSyntax<TOutput> : IEnumerableFluentMapperSyntax<TOutput>
    {
        private readonly IKernel _kernel;

        public IEnumerable<TOutput> From<TInput>(TInput input)
        {
            var converter = _kernel.Get<IMapper<TInput, IEnumerable<TOutput>>>();
            var result = converter.Map(input);
            return result;
        }

        public IEnumerable<TOutput> FromEnumerable<TInput>(IEnumerable<TInput> input)
        {
            if (input == null)
                return Enumerable.Empty<TOutput>();

            var converter = _kernel.Get<IMapper<TInput, TOutput>>();
            var results = input.Select(converter.Map);
            return results;
        }

        public EnumerableFluentMapperSyntax(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}