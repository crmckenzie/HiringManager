using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class AddExistingCandidateToPosition : ITransaction<AddCandidateRequest, AddCandidateResponse>
    {
        private readonly IDbContext _dbContext;

        public AddExistingCandidateToPosition(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddCandidateResponse Execute(AddCandidateRequest request)
        {
            var candidateStatus = AutoMapper.Mapper.Map<CandidateStatus>(request);

            candidateStatus.Candidate = _dbContext.Get<Candidate>(request.CandidateId);

            _dbContext.Add(candidateStatus);

            _dbContext.SaveChanges();

            return new AddCandidateResponse()
                   {
                       CandidateId = candidateStatus.CandidateId.Value,
                       CandidateStatusId = candidateStatus.CandidateStatusId.Value,
                       PositionId = request.PositionId,
                   };
        }
    }
}