using System.Collections.Generic;

namespace HiringManager.Mappers
{
    public interface IFluentMapperSyntax<out TOutput>
    {
        TOutput From<TInput>(TInput input);
        TOutput FromEnumerable<TInput>(IEnumerable<TInput> input);
    }
}