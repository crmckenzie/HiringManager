using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using HiringManager.EntityModel;
using HiringManager.EntityModel.Specifications;
using HiringManager.Transactions;
using Isg.Specification;

namespace HiringManager.DomainServices.Transactions
{
    public class QueryPositionSummaries : ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>
    {
        private readonly IRepository _repository;

        public QueryPositionSummaries(IRepository repository)
        {
            _repository = repository;
        }

        public QueryResponse<PositionSummary> Execute(QueryPositionSummariesRequest request)
        {
            var specification = global::AutoMapper.Mapper.Map<PositionSpecification>(request);

            var query = _repository
                .Query<Position>()
                .Where(specification.IsSatisfied())
                .Project().To<PositionSummary>()
                ;

            var materialized = query.ToList();
            var response = new QueryResponse<PositionSummary>
                           {
                               Data = materialized,
                               Page = 1,
                               PageSize = materialized.Count(),
                               TotalRecords = materialized.Count(),
                           };
            return response;
        }
    }
}