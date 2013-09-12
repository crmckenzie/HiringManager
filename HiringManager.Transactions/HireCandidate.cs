using System;
using System.Linq;
using HiringManager.Domain;
using HiringManager.DomainServices;

namespace HiringManager.Transactions
{
    public class HireCandidate : ITransaction<HireCandidateRequest, HireCandidateResponse>
    {
        private readonly IRepository _repository;
        private readonly IClock _clock;

        public HireCandidate(IRepository repository, IClock clock)
        {
            _repository = repository;
            _clock = clock;
        }

        public HireCandidateResponse Execute(HireCandidateRequest request)
        {
            var candidate = _repository.Get<Candidate>(request.CandidateId);
            var candidatePositionStatus = candidate.AppliedTo.Single(row => row.PositionId == request.PositionId);
            candidatePositionStatus.Status = "Hired";
            candidatePositionStatus.Position.FilledBy = candidate;
            candidatePositionStatus.Position.FilledDate = _clock.Now;
            
            _repository.Commit();

            return new HireCandidateResponse()
                   {
                       CandidateStatusId = candidatePositionStatus.CandidateStatusId,
                       Status = candidatePositionStatus.Status,
                   };
        }
    }
}
