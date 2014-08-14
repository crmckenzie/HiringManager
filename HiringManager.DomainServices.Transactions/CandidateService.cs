using System.Linq;
using AutoMapper.QueryableExtensions;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class CandidateService : DomainServiceBase, ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CandidateService(IFluentTransactionBuilder fluentTransactionBuilder, IUnitOfWork unitOfWork)
            : base(fluentTransactionBuilder)
        {
            _unitOfWork = unitOfWork;
        }

        public CandidateDetails Get(int id)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var candidate = db.Get<Candidate>(id);
                var details = AutoMapper.Mapper.Map<CandidateDetails>(candidate);
                return details;
            }
        }

        public ValidatedResponse Save(SaveCandidateRequest request)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var candidate = AutoMapper.Mapper.Map<SaveCandidateRequest, Candidate>(request);
                db.AddOrUpdate(candidate, candidate.CandidateId);
                db.SaveChanges();

                return new ValidatedResponse();
            }
        }

        public QueryResponse<CandidateSummary> Query(QueryCandidatesRequest request)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var results = db.Query<Candidate>()
                    .Project().To<CandidateSummary>()
                    .ToArray()
                    ;

                return new QueryResponse<CandidateSummary>()
                       {
                           Data = results,
                           Page = 1,
                           PageSize = 1,
                           TotalRecords = results.Length,
                       };

            }
        }

        public QueryResponse<NoteDetails> QueryNotes(QueryNotesRequest request)
        {
            return null;
        }

        public DocumentDetails Upload(UploadDocumentRequest request)
        {
            return base.Execute<UploadDocumentRequest, DocumentDetails>(request);
        }

        public ValidatedResponse Delete(int documentId)
        {
            var transaction = _builder
                .Receives<int>()
                .Returns<ValidatedResponse>()
                .WithRequestValidation()
                .WithPerformanceLogging()
                .Build<DeleteDocument>()
                ;

            var response = transaction.Execute(documentId);

            return response;
        }

        public ValidatedResponse AddNote(AddNoteRequest request)
        {
            return base.Execute<AddNoteRequest, ValidatedResponse>(request);
        }

        public ValidatedResponse EditNote(EditNoteRequest request)
        {
            return null;
        }

        public ValidatedResponse DeleteNote(int noteId)
        {
            return base.Execute<DeleteNoteRequest, ValidatedResponse>(new DeleteNoteRequest()
                                                                      {
                                                                          NoteId = noteId,
                                                                      });
        }
    }
}
