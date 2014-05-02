using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HiringManager.Web.ViewModels.Positions
{
    public class AddCandidateViewModel
    {
        [Required]
        public int PositionId { get; set; }

        [Required]
        [Display(Name = "Candidate")]
        public int? CandidateId { get; set; }

        public SelectList Candidates { get; set; }

    }
}
