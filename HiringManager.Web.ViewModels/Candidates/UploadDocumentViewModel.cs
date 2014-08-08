using System.IO;
using System.Web;

namespace HiringManager.Web.ViewModels.Candidates
{
    public class UploadDocumentViewModel
    {
        public int CandidateId { get; set; }
        public HttpPostedFileBase Document { get; set; }
    }
}