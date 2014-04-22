using System.Collections.Generic;
using Simple.Validation;

namespace HiringManager.DomainServices.Positions
{
    public class CandidateStatusResponse : IValidatedResponse
    {
        public CandidateStatusResponse()
        {
            this.ValidationResults = new ValidationResult[0];
        }

        public int? CandidateStatusId { get; set; }
        public string Status { get; set; }

        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}