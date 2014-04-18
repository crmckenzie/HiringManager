using HiringManager.EntityModel;
using HiringManager.Transactions;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions
{
    public class ClosePosition : ITransaction<int, IValidatedResponse>
    {
        private readonly IRepository _repository;
        private readonly IValidationEngine _validationEngine;

        public ClosePosition(IRepository repository, IValidationEngine validationEngine)
        {
            _repository = repository;
            _validationEngine = validationEngine;
        }

        public IValidatedResponse Execute(int positionId)
        {

            var position = this._repository.Get<Position>(positionId);
            var validationResults = this._validationEngine.Validate(position, "Close");
            if (validationResults.HasErrors())
            {
                return new ValidatedResponse()
                       {
                           ValidationResults = validationResults,
                       };
            }

            position.Status = "Closed";

            foreach (var status in position.Candidates)
                status.Status = "Passed";

            _repository.Commit();

            return new ValidatedResponse()
                   {
                       ValidationResults = validationResults
                   };
        }
    }
}
