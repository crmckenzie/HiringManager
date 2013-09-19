using System.Collections;
using System.Collections.Generic;

namespace HiringManager.Web.Models.Positions
{
    public class PositionCandidateViewModel
    {
        public string Name { get; set; }
        public List<ContactInfoViewModel> ContactInfo { get; set; }
        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }
        public string Status { get; set; }
    }
}