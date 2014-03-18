using System;
using System.ComponentModel.DataAnnotations;

namespace HiringManager.Web.ViewModels.Positions
{
    public class CreatePositionViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Open Date")]
        public DateTime OpenDate { get; set; }
    }
}