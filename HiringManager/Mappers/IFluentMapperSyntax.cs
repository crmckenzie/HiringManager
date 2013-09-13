using System.Collections.Generic;

namespace HiringManager
{
    public interface IFluentMapperSyntax<out TOutput>
    {
        TOutput From<TInput>(TInput input);
        TOutput FromEnumerable<TInput>(IEnumerable<TInput> input);
    }
}