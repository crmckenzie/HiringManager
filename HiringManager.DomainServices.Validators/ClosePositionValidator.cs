using System.Collections.Generic;
using HiringManager.EntityModel;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators
{
    public class ClosePositionValidator : IValidator<Position>
    {
        public bool AppliesTo(string rulesSet)
        {
            return rulesSet == "Close";
        }

        public IEnumerable<ValidationResult> Validate(Position value)
        {
            if (value.IsFilled() || value.IsClosed())
            {
                yield return new ValidationResult()
                             {
                                 Severity = ValidationResultSeverity.Error,
                                 PropertyName = "Status",
                                 Context = value,
                                 Message = "Cannot close a position that is already closed or has been filled.",
                             };
            }
        }
    }
}
