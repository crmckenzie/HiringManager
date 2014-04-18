using System.Collections.Generic;
using HiringManager.EntityModel;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators
{
    public class AddCandidateRequestValidator : IValidator<AddCandidateRequest>
    {
        private readonly IDbContext _dbContext;

        public AddCandidateRequestValidator(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AppliesTo(string rulesSet)
        {
            return true;
        }

        public IEnumerable<ValidationResult> Validate(AddCandidateRequest value)
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
