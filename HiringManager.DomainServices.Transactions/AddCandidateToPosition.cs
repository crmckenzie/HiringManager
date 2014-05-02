using System.Linq;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class AddCandidateToPosition : ITransaction<NewCandidateRequest, NewCandidateResponse>
    {
        private readonly IDbContext _dbContext;

        public AddCandidateToPosition(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public NewCandidateResponse Execute(NewCandidateRequest request)
        {
            var candidateStatus = AutoMapper.Mapper.Map<CandidateStatus>(request);

            if (!request.CandidateId.HasValue)
            {
                _dbContext.Add(candidateStatus.Candidate);

                foreach (var contactInfo in candidateStatus.Candidate.ContactInfo)
                    _dbContext.Add(contactInfo);
            }
            else
            {
                candidateStatus.Candidate = _dbContext.Get<Candidate>(request.CandidateId.Value);
            }

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