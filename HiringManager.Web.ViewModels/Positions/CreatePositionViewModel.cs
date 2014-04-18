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

        [Required]
        [Display(Name = "Number of Openings")]
        [Range(1, Int32.MaxValue)]
        public int Openings { get; set; }
    }
}