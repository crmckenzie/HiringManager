using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiringManager.EntityModel
{
    public class Candidate
    {
        public Candidate()
        {
            this.ContactInfo = new List<ContactInfo>();
            this.AppliedTo = new List<CandidateStatus>();
            this.Documents = new List<Document>();
        }

        public int? CandidateId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public virtual IList<ContactInfo> ContactInfo { get; set; }
        public virtual IList<CandidateStatus> AppliedTo { get; set; }
        public virtual IList<Document> Documents { get; set; }

        public int? SourceId { get; set; }
        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; }
    }
}