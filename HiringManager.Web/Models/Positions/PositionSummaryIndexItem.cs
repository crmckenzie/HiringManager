using System;

namespace HiringManager.Web.Models.Positions
{
    public class PositionSummaryIndexItem
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
        public int CandidatesAwaitingReview { get; set; }
    }
}