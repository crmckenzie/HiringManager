using System.Collections.Generic;
using HiringManager.Domain;

namespace HiringManager.DomainServices
{
    public class AddCandidateRequest
    {
        public int PositionId { get; set; }
        public string CandidateName { get; set; }
        public IList<ContactInfoDetails> ContactInfo { get; set; }
    }
}