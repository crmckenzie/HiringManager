using System.Collections.Generic;

namespace HiringManager
{
    public interface IBuilderSyntax<out TOutput>
    {
        TOutput From<TInput>(TInput input);
        TOutput FromEnumerable<TInput>(IEnumerable<TInput> input);
    }
}