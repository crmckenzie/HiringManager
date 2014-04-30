using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HiringManager.Web.ViewModels.Candidates
{
    public class EditCandidateViewModel
    {
        public int? CandidateId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public ContactInfoViewModel[] ContactInfo { get; set; }

        [Display(Name = "Candidate Source")]
        public int? SourceId { get; set; }
        public string SourceName { get; set; }

        public SelectList Sources { get; set; }
    }
}
