using System.Collections.Generic;
using System.IO;

namespace HiringManager.DomainServices.Positions
{
    public class NewCandidateRequest
    {
        public NewCandidateRequest()
        {
            this.ContactInfo = new List<ContactInfoDetails>();
            this.Documents = new Dictionary<string, Stream>();
        }

        public int PositionId { get; set; }

        public string CandidateName { get; set; }

        public int? SourceId { get; set; }

        public IList<ContactInfoDetails> ContactInfo { get; set; }

        public Dictionary<string, Stream> Documents { get; set; }
    }
}