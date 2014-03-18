using System;

namespace HiringManager.Web.ViewModels.Positions
{
    public class PositionSummaryIndexItem
    {
        public int PositionId { get; set; }
        public string Title { get; set; }
        public string CreatedByName { get; set; }
        public string Status { get; set; }
        public int CandidatesAwaitingReview { get; set; }
        public DateTime OpenDate { get; set; }
        public int? FilledByCandidateId { get; set; }
        public string FilledByName { get; set; }
        public DateTime FilledDate { get; set; }
    }
}