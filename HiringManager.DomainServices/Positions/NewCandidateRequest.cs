using System.Collections.Generic;

namespace HiringManager.DomainServices.Positions
{
    public class NewCandidateRequest
    {
        public NewCandidateRequest()
        {
            this.ContactInfo = new List<ContactInfoDetails>();
        }

        public int PositionId { get; set; }

        public int? CandidateId { get; set; }
        public string CandidateName { get; set; }

        public int? SourceId { get; set; }

        public IList<ContactInfoDetails> ContactInfo { get; set; }
    }
}