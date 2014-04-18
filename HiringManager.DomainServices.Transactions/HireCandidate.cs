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

            var opening = candidateToHire.Position.Openings.First(row => row.FilledBy == null);
            opening.FilledBy = candidateToHire.Candidate;
            opening.FilledDate = _clock.Now;
            opening.Status = "Filled";

            var otherCandidates = candidateToHire.Position.Candidates.ToList();
            otherCandidates.Remove(candidateToHire);
            foreach (var candidateStatus in otherCandidates)
            {
                candidateStatus.Status = "Passed";
            }

            if (candidateToHire.Position.IsFilled())
                candidateToHire.Position.Status = "Filled";

            _dbContext.SaveChanges();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidateToHire.CandidateStatusId,
                       Status = candidateToHire.Status,
                   };
        }
    }
}
