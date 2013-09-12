namespace HiringManager
{
    public interface IFluentMapper
    {
        IBuilderSyntax<TOutput> Map<TOutput>();
        IEnumerableBuilderSyntax<TOutput> MapEnumerable<TOutput>();
    }
}