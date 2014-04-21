using System.ComponentModel.DataAnnotations;

namespace HiringManager.Web.ViewModels.Candidates
{
    public class EditCandidateViewModel
    {
        public int? CandidateId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public ContactInfoViewModel[] ContactInfo { get; set; }

        public int? SourceId { get; set; }
        public string SourceName { get; set; }
    }
}
