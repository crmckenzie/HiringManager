using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class UploadDocument : ITransaction<UploadDocumentRequest, DocumentDetails>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadService _uploadService;

        public UploadDocument(IUnitOfWork unitOfWork, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public DocumentDetails Execute(UploadDocumentRequest request)
        {
            var fileName = this._uploadService.Save(request.Document);

            using (var repository = _unitOfWork.NewDbContext())
            {
                var document = new Document()
                               {
                                   CandidateId = request.CandidateId,
                                   DisplayName = request.FileName,
                                   FileName = fileName
                               };
                repository.Add(document);

                repository.SaveChanges();
                return new DocumentDetails()
                       {
                           DocumentId = document.DocumentId.Value,
                           Title = document.DisplayName,
                       };
            }

        }
    }
}
