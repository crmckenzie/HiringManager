using System.Collections.Generic;

namespace HiringManager.Web.ViewModels.Positions
{
    public class PositionCandidateViewModel
    {
        public string CandidateName { get; set; }
        public List<ContactInfoViewModel> ContactInfo { get; set; }
        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }
        public string Status { get; set; }
        public bool CanPass { get; set; }
        public bool CanSetStatus { get; set; }
        public bool CanHire { get; set; }
    }
}