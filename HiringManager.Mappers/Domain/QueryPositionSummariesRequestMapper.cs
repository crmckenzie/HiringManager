using HiringManager.DomainServices;
using HiringManager.EntityModel;
using HiringManager.EntityModel.Specifications;
using Isg.Specification;

namespace HiringManager.Mappers.Domain
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