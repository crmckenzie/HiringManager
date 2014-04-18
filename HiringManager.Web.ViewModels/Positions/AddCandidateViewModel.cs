using System.ComponentModel.DataAnnotations;

namespace HiringManager.Web.ViewModels.Positions
{
    public class AddCandidateViewModel
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
    }
}