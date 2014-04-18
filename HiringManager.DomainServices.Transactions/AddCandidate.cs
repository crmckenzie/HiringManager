using System.Linq;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class AddCandidate : ITransaction<AddCandidateRequest, AddCandidateResponse>
    {
        private readonly IDbContext _dbContext;

        public AddCandidate(IDbContext dbContext)
        {
            _dbContext = dbContext;
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

            _dbContext.Add(candidate);

            foreach (var contactInfo in candidate.ContactInfo)
                _dbContext.Add(contactInfo);

            _dbContext.Add(candidateStatus);

            _dbContext.SaveChanges();

            return new AddCandidateResponse()
                   {
                       CandidateId = candidate.CandidateId.Value,
                       CandidateStatusId = candidateStatus.CandidateStatusId.Value,
                       PositionId = request.PositionId, 
                   };
        }
    }
}