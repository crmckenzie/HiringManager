using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiringManager.EntityModel
{
    public class Candidate
    {
        public Candidate()
        {
            this.ContactInfo = new List<ContactInfo>();
            this.AppliedTo = new List<CandidateStatus>();
        }

        public int? CandidateId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public virtual IList<ContactInfo> ContactInfo { get; set; } 
        public virtual IList<CandidateStatus> AppliedTo { get; set; } 
    }
}