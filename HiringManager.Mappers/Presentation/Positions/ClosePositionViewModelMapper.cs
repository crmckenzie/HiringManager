using HiringManager.DomainServices;
using HiringManager.Web.ViewModels.Positions;

namespace HiringManager.Mappers.Presentation.Positions
{
    public class ClosePositionViewModelMapper : IMapper<PositionDetails, ClosePositionViewModel>
    {
        public ClosePositionViewModel Map(PositionDetails input)
        {
            return new ClosePositionViewModel()
                   {
                       PositionId = input.PositionId,
                       PositionTitle = input.Title,
                   };
        }
    }
}
