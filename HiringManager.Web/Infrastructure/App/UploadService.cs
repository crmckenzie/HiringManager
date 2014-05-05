using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using HiringManager.DomainServices;

namespace HiringManager.Web.Infrastructure.App
{
    public class UploadService : IUploadService
    {
        public string Save(Stream stream)
        {
            var directory = HttpContext.Current.Server.MapPath("~/App_Upload");
            var fileName = string.Format("{0}.file", Guid.NewGuid());
            var path = System.IO.Path.Combine(directory, fileName);
            var file = new System.IO.FileInfo(path);

            using (var streamReader = new StreamReader(stream))
            using (var fileStream = file.OpenWrite())
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(streamReader.ReadToEnd());
            }
            return fileName;
        }
    }
}