using System.Collections;
using System.Collections.Generic;

namespace HiringManager.Web.Models.Positions
{
    public class PositionCandidatesViewModel
    {
        public string Title { get; set; }
        public List<PositionCandidateViewModel> Candidates { get; set; }
        public int PositionId { get; set; }
    }
}