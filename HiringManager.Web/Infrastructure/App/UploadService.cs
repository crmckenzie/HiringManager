using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Impl;
using HiringManager.EntityModel;

namespace HiringManager.Web.Infrastructure.App
{
    public class UploadService : IUploadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UploadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FileDownload Download(int documentId)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var upload = db.Get<Document>(documentId);
                var directory = HttpContext.Current.Server.MapPath("~/App_Upload");
                var path = System.IO.Path.Combine(directory, upload.FileName);
                var file = new System.IO.FileInfo(path);

                var stream = file.OpenRead();
                return new FileDownload()
                       {
                           FileName = upload.DisplayName,
                           Stream = stream,
                       };

            }
        }

        public string Save(Stream stream)
        {
            var directory = HttpContext.Current.Server.MapPath("~/App_Upload");
            var fileName = string.Format("{0}.file", Guid.NewGuid());
            var path = System.IO.Path.Combine(directory, fileName);
            var file = new System.IO.FileInfo(path);

            using (var fileStream = file.OpenWrite())
            {
                stream.CopyTo(fileStream);
                fileStream.Close();
            }
            return fileName;
        }
    }
}