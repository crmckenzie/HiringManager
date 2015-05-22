using System.Collections.Generic;
using HiringManager.DomainServices.Positions;

namespace HiringManager.DomainServices.Candidates
{
    public class CandidateDetails
    {
        public CandidateDetails()
        {
            this.Documents = new DocumentDetails[0];
        }

        public int CandidateId { get; set; }
        public string Name { get; set; }

        public ContactInfoDetails[] ContactInfo { get; set; }

        public int? SourceId { get; set; }
        public string Source { get; set; }
        public DocumentDetails[] Documents { get; set; }
        public NoteDetails[] Notes { get; set; }
    }
}