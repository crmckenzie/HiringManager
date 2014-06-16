using System.IO;

namespace HiringManager.DomainServices
{
    public class FileDownload
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
    }
}