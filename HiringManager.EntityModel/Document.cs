using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HiringManager.EntityModel
{
    public class Document
    {
        public int? DocumentId { get; set; }

        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; }

        public int CandidateId { get; set; }

        public string FileName { get; set; }

        public string DisplayName { get; set; }
    }
}
