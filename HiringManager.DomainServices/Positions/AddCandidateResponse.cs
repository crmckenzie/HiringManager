using System.Collections.Generic;
using Simple.Validation;

namespace HiringManager.DomainServices.Positions
{
    public class AddCandidateResponse : ValidatedResponse
    {

        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
    }
}