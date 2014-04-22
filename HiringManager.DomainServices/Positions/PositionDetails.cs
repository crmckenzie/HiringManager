using System.Collections.Generic;

namespace HiringManager.DomainServices.Positions
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

        public bool CanAddCandidate { get; set; }
        public bool CanClose { get; set; }

        public IList<CandidateStatusDetails> Candidates { get; set; }
        public int Openings { get; set; }
        public int OpeningsFilled { get; set; }
    }
}