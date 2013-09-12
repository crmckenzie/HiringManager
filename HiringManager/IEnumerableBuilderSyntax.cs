using System.Collections.Generic;

namespace HiringManager
{
    public interface IEnumerableBuilderSyntax<out TOutput>
    {
        IEnumerable<TOutput> From<TInput>(TInput input);
        IEnumerable<TOutput> FromEnumerable<TInput>(IEnumerable<TInput> input);
    }
}