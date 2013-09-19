using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiringManager.Web.Models.Positions
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