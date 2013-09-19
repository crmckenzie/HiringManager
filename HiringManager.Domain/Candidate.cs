using System.Collections.Generic;

namespace HiringManager.Domain
{
    public class Candidate
    {
        public Candidate()
        {
            this.ContactInfo = new List<ContactInfo>();
            this.AppliedTo = new List<CandidateStatus>();
            this.Documents = new List<Document>();
            this.Messages = new List<Message>();
        }

        public int? CandidateId { get; set; }
        public string Name { get; set; }

        public virtual IList<ContactInfo> ContactInfo { get; set; } 
        public virtual IList<CandidateStatus> AppliedTo { get; set; } 
    
        public virtual IList<Document> Documents { get; set; } 

        public virtual IList<Message> Messages { get; set; } 
    }
}