using System.Linq;
using HiringManager.Domain;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class HireCandidate : ITransaction<HireCandidateRequest, CandidateStatusResponse>
    {
        private readonly IRepository _repository;
        private readonly IClock _clock;

        public HireCandidate(IRepository repository, IClock clock)
        {
            _repository = repository;
            _clock = clock;
        }

        public CandidateStatusResponse Execute(HireCandidateRequest request)
        {
            var candidate = _repository.Get<Candidate>(request.CandidateId.Value);
            var candidatePositionStatus = candidate.AppliedTo.Single(row => row.PositionId == request.PositionId);
            candidatePositionStatus.Status = "Hired";
            
            candidatePositionStatus.Position.FilledBy = candidate;
            candidatePositionStatus.Position.FilledDate = _clock.Now;
            candidatePositionStatus.Position.Status = "Filled";

            _repository.Commit();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidatePositionStatus.CandidateStatusId,
                       Status = candidatePositionStatus.Status,
                   };
        }
    }
}
