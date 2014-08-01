using System.Collections.Generic;

namespace HiringManager.DomainServices.Candidates
{
    public class CandidateDetails
    {
        public CandidateDetails()
        {
            this.Documents = new DocumentItem[0];
        }

        public int CandidateId { get; set; }
        public string Name { get; set; }

        public ContactInfoDetails[] ContactInfo { get; set; }

        public int? SourceId { get; set; }
        public string Source { get; set; }
        public DocumentItem[] Documents { get; set; }
    }
}