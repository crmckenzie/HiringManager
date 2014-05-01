using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
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

        public CandidateDetails Get(int id)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var candidate = db.Get<Candidate>(id);
                var details = AutoMapper.Mapper.Map<CandidateDetails>(candidate);
                return details;
            }
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

        public QueryResponse<CandidateSummary> Query(QueryCandidatesRequest request)
        {
            using (var db = _unitOfWork.NewDbContext())
            {
                var results = db.Query<Candidate>()
                    .Project().To<CandidateSummary>()
                    .ToArray()
                    ;
                
                return new QueryResponse<CandidateSummary>()
                       {
                           Data = results,
                           Page = 1,
                           PageSize = 1,
                           TotalRecords = results.Length,
                       };

            }
        }
    }
}
