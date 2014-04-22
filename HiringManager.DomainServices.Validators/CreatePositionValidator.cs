using System.Collections.Generic;
using HiringManager.DomainServices.Positions;
using Simple.Validation;
using Simple.Validation.Validators;

namespace HiringManager.DomainServices.Validators
{
    public class CreatePositionValidator: CompositeValidator<CreatePositionRequest>
    {
        public override bool AppliesTo(string rulesSet)
        {
            return true;
        }

        protected override IEnumerable<IValidator<CreatePositionRequest>> GetInternalValidators()
        {
            yield return Properties<CreatePositionRequest>
                .For(req => req.Openings)
                .GreaterThanOrEqualTo(1)
                .Message("Openings is required")
                ;
        }
    }
}