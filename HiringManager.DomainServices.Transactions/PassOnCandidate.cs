using System;
using System.Linq;
using HiringManager.Domain;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class PassOnCandidate : ITransaction<PassOnCandidateRequest, CandidateStatusResponse>
    {
        private readonly IRepository _repository;
        private readonly IClock _clock;

        public PassOnCandidate(IRepository repository, IClock clock)
        {
            _repository = repository;
            _clock = clock;
        }

        public CandidateStatusResponse Execute(PassOnCandidateRequest request)
        {
            var candidateStatus = _repository.Get<CandidateStatus>(request.CandidateStatusId);
            candidateStatus.Status = "Passed";
            
            _repository.Commit();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidateStatus.CandidateStatusId,
                       Status = candidateStatus.Status,
                   };
        }
    }
}