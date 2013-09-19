using System;

namespace HiringManager.Web.Models.Positions
{
    public class PositionSummaryIndexItem
    {
        public int PositionId { get; set; }
        public string Title { get; set; }
        public string CreatedByName { get; set; }
        public string Status { get; set; }
        public int CandidatesAwaitingReview { get; set; }
        public DateTime OpenDate { get; set; }
    }
}