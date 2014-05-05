using System.IO;

namespace HiringManager.DomainServices
{
    public interface IUploadService
    {
        string Save(Stream stream);
    }
}