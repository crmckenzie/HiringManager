using System.Collections.Generic;

namespace HiringManager.Mappers
{
    public interface IEnumerableFluentMapperSyntax<out TOutput>
    {
        IEnumerable<TOutput> From<TInput>(TInput input);
        IEnumerable<TOutput> FromEnumerable<TInput>(IEnumerable<TInput> input);
    }
}