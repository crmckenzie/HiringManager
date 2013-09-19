using HiringManager.Domain;
using WebMatrix.WebData;

namespace HiringManager.DomainServices.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;

        public AccountService(IRepository repository)
        {
            _repository = repository;
        }

        public int? Register(RegisterManagerRequest registerManagerRequest)
        {
            WebSecurity.CreateUserAndAccount(registerManagerRequest.UserName, registerManagerRequest.Password);
            WebSecurity.Login(registerManagerRequest.UserName, registerManagerRequest.Password);

            var hiringManager = new Manager()
                                {
                                    Name = registerManagerRequest.DisplayName,
                                    UserName = registerManagerRequest.UserName,
                                    Title = registerManagerRequest.Title,
                                };

            _repository.Store(hiringManager);

            _repository.Commit();

            return hiringManager.ManagerId;

        }

    }
}