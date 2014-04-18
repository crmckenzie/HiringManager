using System.Collections.Generic;
using Simple.Validation;

namespace HiringManager.DomainServices
{
    public class ValidatedResponse : IValidatedResponse
    {
        public ValidatedResponse()
        {
            this.ValidationResults = new ValidationResult[0];
        }

        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}