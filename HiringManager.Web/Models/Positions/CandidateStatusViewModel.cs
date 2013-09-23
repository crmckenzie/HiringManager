using System.ComponentModel.DataAnnotations;

namespace HiringManager.Web.Models.Positions
{
    public class CandidateStatusViewModel
    {
        public int CandidateStatusId { get; set; }
        public int PositionId { get; set; }
        [Display(Name = "Position Title")]
        public string PositionTitle { get; set; }

        public int CandidateId { get; set; }
        public string CandidateName { get; set; }

        public string Status { get; set; }
    }
}