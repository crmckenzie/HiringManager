﻿using System;

namespace HiringManager.DomainServices
{
    public class PositionSummary
    {
        public int? PositionId { get; set; }

        public int? CreatedById { get; set; }
        public string CreatedByName { get; set; }

        public int? FilledByCandidateId { get; set; }
        public string FilledByName { get; set; }

        public DateTime? FilledDate { get; set; }
        public DateTime OpenDate { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public int CandidatesAwaitingReview { get; set; }
    }
}