using System.Linq;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class HireCandidate : ITransaction<HireCandidateRequest, CandidateStatusResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IClock _clock;

        public HireCandidate(IDbContext dbContext, IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public CandidateStatusResponse Execute(HireCandidateRequest request)
        {
            var candidateToHire = _dbContext.Get<CandidateStatus>(request.CandidateStatusId);
            candidateToHire.Status = "Hired";
            
            candidateToHire.Position.FilledBy = candidateToHire.Candidate;
            candidateToHire.Position.FilledDate = _clock.Now;
            candidateToHire.Position.Status = "Filled";

            var otherCandidates = candidateToHire.Position.Candidates.ToList();
            otherCandidates.Remove(candidateToHire);
            foreach (var candidateStatus in otherCandidates)
            {
                candidateStatus.Status = "Passed";
            }

            _dbContext.SaveChanges();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidateToHire.CandidateStatusId,
                       Status = candidateToHire.Status,
                   };
        }
    }
}
