using HiringManager.DomainServices.Authentication;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using HiringManager.Transactions;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions
{
    public class AddNote : ITransaction<AddNoteRequest, ValidatedResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSession _userSession;
        private readonly IClock _clock;
        private readonly IValidationEngine _validationEngine;

        public AddNote(IUnitOfWork unitOfWork, IUserSession userSession, IClock clock, IValidationEngine validationEngine)
        {
            _unitOfWork = unitOfWork;
            _userSession = userSession;
            _clock = clock;
            _validationEngine = validationEngine;
        }

        public ValidatedResponse Execute(AddNoteRequest request)
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
                db.Add(new Note()
                       {
                           AuthorId = _userSession.ManagerId,
                           Authored = _clock.Now,
                           Text = request.Text,
                           CandidateStatusId = request.CandidateStatusId,
                       });

                db.SaveChanges();
            }

            return response;
        }
    }
}
