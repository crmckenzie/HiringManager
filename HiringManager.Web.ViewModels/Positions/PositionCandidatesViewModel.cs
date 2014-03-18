using System.Collections.Generic;

namespace HiringManager.Web.ViewModels.Positions
{
    public class PositionCandidatesViewModel
    {
        public string Title { get; set; }
        public List<PositionCandidateViewModel> Candidates { get; set; }
        public int PositionId { get; set; }
        public string Status { get; set; }

        public bool CanAddCandidate { get; set; }
        public bool CanClose { get; set; }
    }
}