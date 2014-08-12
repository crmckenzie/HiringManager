namespace HiringManager.DomainServices.Authentication
{
    public interface IUserSession
    {
        int ManagerId { get; }
        string UserName { get; }
        string DisplayName { get; }
    }
}
