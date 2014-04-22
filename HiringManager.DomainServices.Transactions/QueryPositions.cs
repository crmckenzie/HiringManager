using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.EntityModel.Specifications;
using HiringManager.Transactions;
using Isg.Specification;

namespace HiringManager.DomainServices.Transactions
{
    public class QueryPositionSummaries : ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>
    {
        private readonly IDbContext _dbContext;

        public QueryPositionSummaries(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public QueryResponse<PositionSummary> Execute(QueryPositionSummariesRequest request)
        {
            var specification = global::AutoMapper.Mapper.Map<PositionSpecification>(request);

            var query = _dbContext
                .Query<Position>()
                .Where(specification.IsSatisfied())
                //.Project().To<PositionSummary>()
                ;

            var projection = query.ToList().Select(AutoMapper.Mapper.Map<PositionSummary>);

            var materialized = projection.ToList();
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