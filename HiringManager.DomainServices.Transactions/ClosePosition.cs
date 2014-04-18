using HiringManager.EntityModel;
using HiringManager.Transactions;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions
{
    public class ClosePosition : ITransaction<int, IValidatedResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IValidationEngine _validationEngine;

        public ClosePosition(IDbContext dbContext, IValidationEngine validationEngine)
        {
            _dbContext = dbContext;
            _validationEngine = validationEngine;
        }

        public IValidatedResponse Execute(int positionId)
        {

            var position = this._dbContext.Get<Position>(positionId);
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

            _dbContext.SaveChanges();

            return new ValidatedResponse()
                   {
                       ValidationResults = validationResults
                   };
        }
    }
}
