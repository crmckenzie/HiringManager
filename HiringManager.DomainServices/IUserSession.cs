namespace HiringManager.DomainServices
{
    public interface IUserSession
    {
        int? ManagerId { get; }
        string UserName { get; }
        string DisplayName { get;  }
    }
}
