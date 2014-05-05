using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager.EntityModel
{
    public class Document
    {
        public int? DocumentId { get; set; }

        public Candidate Candidate { get; set; }

        public int CandidateId { get; set; }

        public string FileName { get; set; }

        public string DisplayName { get; set; }
    }
}
