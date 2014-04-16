using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class SetCandidateStatus : ITransaction<SetCandidateStatusRequest, CandidateStatusResponse>
    {
        private readonly IRepository _repository;

        public SetCandidateStatus(IRepository repository)
        {
            _repository = repository;
        }

        public CandidateStatusResponse Execute(SetCandidateStatusRequest request)
        {
            var candidateStatus = _repository.Get<CandidateStatus>(request.CandidateStatusId);
            candidateStatus.Status = request.Status;
            
            _repository.Commit();

            return new CandidateStatusResponse()
                   {
                       CandidateStatusId = candidateStatus.CandidateStatusId,
                       Status = candidateStatus.Status,
                   };
        }
    }
}