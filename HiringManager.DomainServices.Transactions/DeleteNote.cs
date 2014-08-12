using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using HiringManager.Transactions;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions
{
    public class DeleteNote : ITransaction<DeleteNoteRequest, ValidatedResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationEngine _validationEngine;

        public DeleteNote(IUnitOfWork unitOfWork, IValidationEngine validationEngine)
        {
            _unitOfWork = unitOfWork;
            _validationEngine = validationEngine;
        }

        public ValidatedResponse Execute(DeleteNoteRequest request)
        {
            var results = this._validationEngine.Validate(request);
            var response = new ValidatedResponse()
                           {
                               ValidationResults = results,
                           };
            if (response.HasErrors())
            {
                return response;
            }

            using (var db = _unitOfWork.NewDbContext())
            {
                db.Delete(new Note()
                          {
                              NoteId = request.NoteId,
                          });

                db.SaveChanges();
            }

            return response;
        }
    }
}