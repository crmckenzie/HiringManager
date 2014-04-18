using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiringManager.EntityModel
{
    public class Position
    {
        public Position()
        {
            this.Candidates = new List<CandidateStatus>();
        }

        public int? PositionId { get; set; }

        public int? FilledById { get; set; }

        [ForeignKey("FilledById")]
        public virtual Candidate FilledBy { get; set; }

        public int CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Manager CreatedBy { get; set; }

        public virtual List<CandidateStatus> Candidates { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? FilledDate { get; set; }

        public virtual bool IsFilled()
        {
            return this.FilledBy != null;
        }

        public virtual bool IsClosed()
        {
            return this.Status == "Closed";
        }
    }
}