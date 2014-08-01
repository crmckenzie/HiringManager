using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HiringManager.Web.ViewModels.Candidates
{
    public class CandidateDetailsViewModel
    {
        public CandidateDetailsViewModel()
        {
            ContactInfo = new ContactInfoViewModel[0];
            Documents = new SelectListItem[0];
        }
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public int? SourceId { get; set; }
        public SelectListItem[] Documents { get; set; }
        public ContactInfoViewModel[] ContactInfo { get; set; }

    }
}
