﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiringManager.EntityModel
{
    public class CandidateStatus
    {
        public int? CandidateStatusId { get; set; }
        
        [ForeignKey("Candidate")]
        public int? CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("Position")]
        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
