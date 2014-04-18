using System.Linq;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class AddCandidate : ITransaction<AddCandidateRequest, AddCandidateResponse>
    {
        private readonly IRepository _repository;

        public AddCandidate(IRepository repository)
        {
            _repository = repository;
        }

        public AddCandidateResponse Execute(AddCandidateRequest request)
        {
            var candidate = new Candidate()
                            {
                                Name = request.CandidateName,
                            };
            candidate.ContactInfo = request.ContactInfo.Select(row => new ContactInfo()
                                                                 {
                                                                     Type = row.Type,
                                                                     Value = row.Value,
                                                                     Candidate = candidate,
                                                                 }).ToList();

            var candidateStatus = new CandidateStatus()
                                  {
                                      PositionId = request.PositionId,
                                      Status = "Resume Received",
                                      Candidate = candidate
                                  };

            _repository.Store(candidate);

            foreach (var contactInfo in candidate.ContactInfo)
                _repository.Store(contactInfo);

            _repository.Store(candidateStatus);

            _repository.Commit();

            return new AddCandidateResponse()
                   {
                       CandidateId = candidate.CandidateId.Value,
                       CandidateStatusId = candidateStatus.CandidateStatusId.Value,
                       PositionId = request.PositionId, 
                   };
        }
    }
}