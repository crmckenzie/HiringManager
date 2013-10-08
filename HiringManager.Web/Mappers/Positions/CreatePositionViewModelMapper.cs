using HiringManager.DomainServices;
using HiringManager.Mappers;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Mappers.Positions
{
    public class CreatePositionViewModelMapper : IMapper<CreatePositionViewModel, CreatePositionRequest>
    {
        private readonly IUserSession _userSession;

        public CreatePositionViewModelMapper(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public CreatePositionRequest Map(CreatePositionViewModel input)
        {
            var result = AutoMapper.Mapper.DynamicMap<CreatePositionViewModel, CreatePositionRequest>(input);
            result.HiringManagerId = _userSession.ManagerId;
            return result;
        }
    }
}