using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HiringManager.Web.ViewModels.Positions
{
    public class CandidateStatusViewModel
    {
        public int CandidateStatusId { get; set; }
        public int PositionId { get; set; }

        [Display(Name = "Position Title")]
        public string PositionTitle { get; set; }

        public int CandidateId { get; set; }

        [Display(Prompt = "Candidate Name")]
        public string CandidateName { get; set; }

        public int? SourceId { get; set; }

        [DisplayName("Source")]
        public string SourceName { get; set; }

        public string Status { get; set; }
    }
}