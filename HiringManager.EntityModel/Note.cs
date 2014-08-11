using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringManager.EntityModel
{
    public class Note
    {
        public int? NoteId { get; set; }

        public int CandidateStatusId { get; set; }

        [ForeignKey("CandidateStatusId")]
        public CandidateStatus Status { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string Text { get; set; }

        [ForeignKey("AuthorId")]
        public Manager Author { get; set; }

        public int AuthorId { get; set; }

        public DateTime Authored { get; set; }
    }
}
