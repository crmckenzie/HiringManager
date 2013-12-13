using System.Linq;
using HiringManager.DomainServices;
using HiringManager.Transactions;
using Simple.Validation;

namespace HiringManager.Web.Ninject.Transactions
{
    public class RequestValidationStep<TRequest, TResponse> : TransactionStepBase<TRequest, TResponse>
    {
        private readonly IValidationEngine _validationEngine;
        private readonly string[] _ruleSets;

        public RequestValidationStep(ITransaction<TRequest, TResponse> innerCommand, IValidationEngine validationEngine, params string[] ruleSets)
            : base(innerCommand)
        {
            _validationEngine = validationEngine;
            _ruleSets = ruleSets;
        }

        public override TResponse Execute(TRequest request)
        {
            var isValidatedResponse = typeof(TResponse).Implements<IValidatedResponse>();
            if (isValidatedResponse)
            {
                var requestValidationResults = _validationEngine.Validate(request, _ruleSets).ToArray();
                if (requestValidationResults.HasErrors())
                {
                    var preemptiveResponse = System.Activator.CreateInstance<TResponse>();
                    (preemptiveResponse as IValidatedResponse).ValidationResults = requestValidationResults;
                    return preemptiveResponse;
                }

                var validatedResponse = base.InnerCommand.Execute(request) as IValidatedResponse;

                validatedResponse.ValidationResults = validatedResponse.ValidationResults == null ?
                    requestValidationResults :
                    requestValidationResults.Concat(validatedResponse.ValidationResults);

                return (TResponse)validatedResponse;
            }


            _validationEngine.Enforce(request);
            var response = base.InnerCommand.Execute(request);
            return response;
        }
    }
}