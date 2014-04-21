using System.Collections.Generic;

namespace HiringManager.DomainServices
{
    public class AddCandidateRequest
    {
        public AddCandidateRequest()
        {
            this.ContactInfo = new List<ContactInfoDetails>();
        }

        public int PositionId { get; set; }
        public string CandidateName { get; set; }

        public int? SourceId { get; set; }

        public IList<ContactInfoDetails> ContactInfo { get; set; }
    }
}