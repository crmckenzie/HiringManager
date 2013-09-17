namespace HiringManager.DomainServices
{
    public interface IAccountService
    {
        int? Register(RegisterManagerRequest registerManagerRequest);
    }
}
