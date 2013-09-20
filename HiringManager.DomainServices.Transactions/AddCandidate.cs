using System;
using System.Linq;
using HiringManager.Domain;
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
            var contactInfos = request.ContactInfo.Select(row => new ContactInfo()
                                                                 {
                                                                     Type = row.Type,
                                                                     Value = row.Value,
                                                                 }).ToList();
            var candidate = new Candidate()
                            {
                                Name = request.CandidateName,
                                ContactInfo = contactInfos,
                            };

            foreach (var contactInfo in contactInfos)
            {
                contactInfo.Candidate = candidate;
            }

            var candidateStatus = new CandidateStatus()
                                  {
                                      PositionId = request.PositionId,
                                      Status = "Resume Received",
                                      Candidate = candidate
                                  };


            _repository.Store(candidate);

            foreach (var contactInfo in contactInfos)
            {
                _repository.Store(contactInfo);
            }

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