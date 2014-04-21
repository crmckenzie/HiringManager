using AutoMapper.QueryableExtensions;
using HiringManager.EntityModel;
using System.Linq;

namespace HiringManager.DomainServices.Impl
{
    public class SourceService : ISourceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public QueryResponse<SourceSummary> Query(QuerySourcesRequest request)
        {
            using (var repository = _unitOfWork.NewDbContext())
            {
                var results = repository.Query<Source>()
                    .Project().To<SourceSummary>()
                    .ToArray()
                    ;

                var response = new QueryResponse<SourceSummary>()
                               {
                                   Data = results,
                                   Page = 1,
                                   PageSize = results.Length,
                                   TotalRecords = results.Length
                               };
                return response;

            }
        }
    }
}