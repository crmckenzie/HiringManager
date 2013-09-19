using System.Collections.Generic;

namespace HiringManager.DomainServices
{
    public class PositionDetails
    {
        public PositionDetails()
        {
            this.Candidates = new List<CandidateStatusDetails>();
        }

        public int PositionId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public IList<CandidateStatusDetails>  Candidates { get; set; }
    }
}