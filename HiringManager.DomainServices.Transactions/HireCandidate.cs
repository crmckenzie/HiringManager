using System.Linq;
using HiringManager.EntityModel;
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
            var candidateStatus = _repository.Get<CandidateStatus>(request.CandidateStatusId);
            var candidate = candidateStatus.Candidate;
            candidateStatus.Status = "Hired";
            
            candidateStatus.Position.FilledBy = candidate;
            candidateStatus.Position.FilledDate = _clock.Now;
            candidateStatus.Position.Status = "Filled";

            _repository.Commit();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidateStatus.CandidateStatusId,
                       Status = candidateStatus.Status,
                   };
        }
    }
}
