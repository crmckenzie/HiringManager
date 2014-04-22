using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class SetCandidateStatus : ITransaction<SetCandidateStatusRequest, CandidateStatusResponse>
    {
        private readonly IDbContext _dbContext;

        public SetCandidateStatus(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CandidateStatusResponse Execute(SetCandidateStatusRequest request)
        {
            var candidateStatus = _dbContext.Get<CandidateStatus>(request.CandidateStatusId);
            candidateStatus.Status = request.Status;
            
            _dbContext.SaveChanges();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidateStatus.CandidateStatusId,
                       Status = candidateStatus.Status,
                   };
        }
    }
}