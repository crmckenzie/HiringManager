namespace HiringManager.Mappers
{
    public interface IFluentMapper
    {
        IFluentMapperSyntax<TOutput> Map<TOutput>();
        IEnumerableFluentMapperSyntax<TOutput> MapEnumerable<TOutput>();
    }
}