using HiringManager.Domain;
using WebMatrix.WebData;

namespace HiringManager.Web.ApplicationServices
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;

        public AccountService(IRepository repository)
        {
            _repository = repository;
        }

        public int? Register(RegisterModel model)
        {
            WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
            WebSecurity.Login(model.UserName, model.Password);

            var hiringManager = new Manager()
                                {
                                    Name = model.DisplayName,
                                    UserName = model.UserName,
                                    Title = model.Title,
                                };

            _repository.Store(hiringManager);

            _repository.Commit();

            return hiringManager.ManagerId;

        }
    }
}