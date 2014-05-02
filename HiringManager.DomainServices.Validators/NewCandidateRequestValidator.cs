using System.Collections.Generic;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators
{
    public class NewCandidateRequestValidator : IValidator<NewCandidateRequest>
    {
        private readonly IDbContext _dbContext;

        public NewCandidateRequestValidator(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AppliesTo(string rulesSet)
        {
            return true;
        }

        public IEnumerable<ValidationResult> Validate(NewCandidateRequest value)
        {
            var position = _dbContext.Get<Position>(value.PositionId);
            if (position.IsFilled())
            {
                yield return new ValidationResult()
                             {
                                 PropertyName = "PositionId",
                                 Severity = ValidationResultSeverity.Error,
                                 Message = "Cannot add a candidate to a filled position.",
                             };
            }
        }
    }
}
