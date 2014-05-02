using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HiringManager.Web.ViewModels.Positions
{
    public class NewCandidateViewModel
    {
        [Required]
        public int PositionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Candidate Sourced By")]
        public int? SourceId { get; set; }

        public int? CandidateId { get; set; }

        public SelectList Sources { get; set; }
    }
}