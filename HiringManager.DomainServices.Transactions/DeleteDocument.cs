using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringManager.DomainServices.Impl;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class DeleteDocument : ITransaction<int, ValidatedResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadService _uploadService;

        public DeleteDocument(IUnitOfWork unitOfWork, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public ValidatedResponse Execute(int request)
        {
            using (var repository = _unitOfWork.NewDbContext())
            {
                var document = repository.Get<Document>(request);
                repository.Delete(document);
                repository.SaveChanges();

                this._uploadService.Delete(document.FileName);
            }

            return new ValidatedResponse();
        }
    }
}
