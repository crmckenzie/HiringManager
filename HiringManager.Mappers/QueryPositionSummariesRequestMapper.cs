using System;
using HiringManager.Domain.Specifications;
using HiringManager.DomainServices;
using Isg.Specification;

namespace HiringManager.Domain.Mappers
{
    public class QueryPositionSummariesRequestMapper : IMapper<QueryPositionSummariesRequest, ISpecification<Position>>
    {
        public ISpecification<Position> Map(QueryPositionSummariesRequest input)
        {
            if (input == null)
                return new PositionSpecification();

            var result = AutoMapper.Mapper.DynamicMap<QueryPositionSummariesRequest, PositionSpecification>(input);
            return result;
        }
    }
}