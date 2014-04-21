using System.Collections.Generic;

namespace HiringManager.DomainServices
{
    public class CandidateStatusDetails
    {
        public CandidateStatusDetails()
        {
            this.ContactInfo = new List<ContactInfoDetails>();
        }

        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }

        public int PositionId { get; set; }
        public string PositionTitle { get; set; }

        public int? SourceId { get; set; }
        public string SourceName { get; set; }

        public string CandidateName { get; set; }
        public string Status { get; set; }

        public IList<ContactInfoDetails> ContactInfo { get; set; }
        public bool CanHire { get; set; }
        public bool CanPass { get; set; }
        public bool CanSetStatus { get; set; }
    }
}