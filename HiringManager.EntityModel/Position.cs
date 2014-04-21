using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HiringManager.EntityModel
{
    public class Position
    {
        public Position()
        {
            this.Candidates = new List<CandidateStatus>();
            this.Openings = new List<Opening>();
        }

        public int? PositionId { get; set; }

        public int CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Manager CreatedBy { get; set; }

        public virtual List<CandidateStatus> Candidates { get; set; }

        public virtual List<Opening> Openings { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public DateTime OpenDate { get; set; }

        public virtual bool IsFilled()
        {
            return Openings.Any() && Openings.All(row => row.IsFilled());
        }

        public virtual bool IsClosed()
        {
            return this.Status == "Closed";
        }

        public void Add(Candidate candidate)
        {
            var status = new CandidateStatus()
                         {
                             Position = this,
                             Candidate = candidate,
                             Status = "Resume Received",
                         };
            this.Candidates.Add(status);
        }
    }
}