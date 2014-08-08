using System.IO;

namespace HiringManager.DomainServices.Candidates
{
    public class UploadDocumentRequest
    {
        public int CandidateId { get; set; }
        public string FileName { get; set; }
        public Stream Document { get; set; }
    }
}