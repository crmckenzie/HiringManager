using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiringManager.Domain
{
    public class Position
    {
        public Position()
        {
            this.Candidates = new List<CandidateStatus>();
        }

        public int? PositionId { get; set; }

        public int? FilledById { get; set; }
        public virtual Candidate FilledBy { get; set; }

        [Required]
        public int CreatedById { get; set; }

        public virtual Manager CreatedBy { get; set; }

        public virtual IList<CandidateStatus> Candidates { get; set; }

        [Required]
        public string Status { get; set; }

        public string Title { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? FilledDate { get; set; }

    }
}