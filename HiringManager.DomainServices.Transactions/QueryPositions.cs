using System;
using System.Linq;
using HiringManager.Domain;
using HiringManager.Mappers;
using HiringManager.Transactions;
using Isg.Specification;

namespace HiringManager.DomainServices.Transactions
{
    public class QueryPositionSummaries : ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>
    {
        private readonly IRepository _repository;
        private readonly IFluentMapper _fluentMapper;

        public QueryPositionSummaries(IRepository repository, IFluentMapper fluentMapper)
        {
            _repository = repository;
            _fluentMapper = fluentMapper;
        }

        public QueryResponse<PositionSummary> Execute(QueryPositionSummariesRequest request)
        {
            var specification = this._fluentMapper
                .Map<ISpecification<Position>>()
                .From(request)
                ;

            var query = _repository.Query<Position>()
                .Where(specification.IsSatisfied())
                ;

            var summaries = _fluentMapper
                .MapEnumerable<PositionSummary>()
                .FromEnumerable(query)
                ;

            var materialized = summaries.ToList();
            var response = new QueryResponse<PositionSummary>
                           {
                               Data = materialized,
                               Page = 1,
                               PageSize = summaries.Count(),
                               TotalRecords = summaries.Count(),
                           };
            return response;
        }
    }
}