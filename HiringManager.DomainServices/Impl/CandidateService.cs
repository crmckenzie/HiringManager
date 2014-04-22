using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;

namespace HiringManager.DomainServices.Impl
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CandidateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ValidatedResponse Save(SaveCandidateRequest request)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var candidate = AutoMapper.Mapper.Map<SaveCandidateRequest, Candidate>(request);
                db.AddOrUpdate(candidate, candidate.CandidateId);
                db.SaveChanges();

                return new ValidatedResponse();
            }
        }
    }
}
