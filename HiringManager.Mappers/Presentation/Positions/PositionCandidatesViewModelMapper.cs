using HiringManager.DomainServices;
using HiringManager.Web.ViewModels.Positions;

namespace HiringManager.Mappers.Presentation.Positions
{
    public class PositionCandidatesViewModelMapper : IMapper<PositionDetails, PositionCandidatesViewModel>
    {
        public PositionCandidatesViewModel Map(PositionDetails input)
        {
            var result = AutoMapper.Mapper.DynamicMap<PositionDetails, PositionCandidatesViewModel>(input);
            return result;
        }
    }
}