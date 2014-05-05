using System.Collections.Generic;

namespace HiringManager.DomainServices.Positions
{
    public class AddCandidateRequest
    {
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
    }
}