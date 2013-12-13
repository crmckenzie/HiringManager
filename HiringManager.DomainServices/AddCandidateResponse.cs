using System.Collections.Generic;
using Simple.Validation;

namespace HiringManager.DomainServices
{
    public class AddCandidateResponse : IValidatedResponse
    {
        public AddCandidateResponse()
        {
            this.ValidationResults = new ValidationResult[0];
        }

        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}