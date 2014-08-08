using System.IO;

namespace HiringManager.DomainServices
{
    public interface IUploadService
    {
        FileDownload Download(int documentId);
        string Save(Stream stream);
        void Delete(string fileName);
    }
}