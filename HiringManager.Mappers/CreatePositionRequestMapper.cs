using HiringManager.Domain;
using HiringManager.DomainServices;

namespace HiringManager.Mappers
{
    public class CreatePositionRequestMapper : IMapper<CreatePositionRequest, Position>
    {
        public Position Map(CreatePositionRequest input)
        {
            var result = AutoMapper.Mapper.DynamicMap<CreatePositionRequest, Position>(input);
            result.CreatedById = input.HiringManagerId.Value;
            result.Status = "Open";
            return result;
        }
    }
}
