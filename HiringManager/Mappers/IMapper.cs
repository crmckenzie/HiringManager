namespace HiringManager.Mappers
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput Map(TInput input);
    }
}
