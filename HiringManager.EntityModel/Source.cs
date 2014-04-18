using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiringManager.EntityModel
{
    public class Source
    {
        public int? SourceId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual List<Candidate> Candidates { get; set; } 
    }
}