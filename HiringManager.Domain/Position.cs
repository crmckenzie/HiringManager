using System;
using System.Collections.Generic;

namespace HiringManager.Domain
{
    public class Position
    {
        public Position()
        {
            this.Candidates = new List<CandidateStatus>();
        }

        public int? PositionId { get; set; }
        public string Title { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? FilledDate { get; set; }

        public Candidate FilledBy { get; set; }
        public HiringManager CreatedBy { get; set; }

        public IList<CandidateStatus> Candidates { get; set; } 
    }
}