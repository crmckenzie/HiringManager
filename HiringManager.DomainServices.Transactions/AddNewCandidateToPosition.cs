using System.Linq;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class AddNewCandidateToPosition : ITransaction<NewCandidateRequest, NewCandidateResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUploadService _uploadService;

        public AddNewCandidateToPosition(IDbContext dbContext, IUploadService uploadService)
        {
            _dbContext = dbContext;
            _uploadService = uploadService;
        }

        public NewCandidateResponse Execute(NewCandidateRequest request)
        {
            var candidateStatus = AutoMapper.Mapper.Map<CandidateStatus>(request);

            _dbContext.Add(candidateStatus.Candidate);

            foreach (var document in request.Documents)
            {
                var fileName = _uploadService.Save(document.Value);
                _dbContext.Add(new Document()
                               {
                                   CandidateId = candidateStatus.Candidate.CandidateId.Value,
                                   DisplayName = document.Key,
                                   FileName = fileName,
                               });
            }

            foreach (var contactInfo in candidateStatus.Candidate.ContactInfo)
                _dbContext.Add(contactInfo);

            _dbContext.Add(candidateStatus);


            _dbContext.SaveChanges();

            return new NewCandidateResponse()
                   {
                       CandidateId = candidateStatus.CandidateId.Value,
                       CandidateStatusId = candidateStatus.CandidateStatusId.Value,
                       PositionId = request.PositionId,
                   };
        }
    }
}