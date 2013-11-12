
using System.ComponentModel.DataAnnotations;

namespace HiringManager.EntityModel
{
    public class CandidateStatus
    {
        public int? CandidateStatusId { get; set; }
        
        public int? CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
