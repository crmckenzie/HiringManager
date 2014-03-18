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
            var converter = GetConverter<TInput>();
            var result = converter.Map(input);
            return result;
        }

        private IMapper<TInput, TOutput> GetConverter<TInput>()
        {
            var converter = _kernel.TryGet<IMapper<TInput, TOutput>>();
            if (converter == null)
                converter = new DefaultMapper<TInput, TOutput>();
            return converter;
        }


        private class DefaultMapper<TInput, TOutput> : IMapper<TInput, TOutput>
        {
            public TOutput Map(TInput input)
            {
                return AutoMapper.Mapper.DynamicMap<TOutput>(input);
            }
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