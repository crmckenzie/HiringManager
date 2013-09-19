using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HiringManager.DomainServices
{
    public class RegisterManagerRequest
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}