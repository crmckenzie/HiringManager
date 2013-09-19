using System.Linq;
using System.Security.Principal;
using HiringManager.Transactions;

namespace HiringManager.Web.Ninject.Transactions
{
    public class AuthorizationStep<TRequest, TResponse> : TransactionStepBase<TRequest, TResponse>
    {
        private readonly IPrincipal _principal;
        private readonly string[] _roles;

        public AuthorizationStep(ITransaction<TRequest, TResponse> innerCommand, IPrincipal principal, params string[] roles)
            : base(innerCommand)
        {
            _principal = principal;
            _roles = roles;
        }

        public override TResponse Execute(TRequest request)
        {
            if (_roles.Any())
            {
                var isAuthorized = _roles.Any(role => _principal.IsInRole(role));
                if (!isAuthorized)
                    throw new AuthorizationException();

                var result = base.InnerCommand.Execute(request);
                return result;
                
            }
            else
            {
                var result = base.InnerCommand.Execute(request);
                return result;
            }

        }
    }
}