using HiringManager.DomainServices;
using HiringManager.Web.ViewModels.Positions;

namespace HiringManager.Mappers.Presentation.Positions
{
    public class CandidateStatusViewModelMapper : IMapper<CandidateStatusDetails, CandidateStatusViewModel>
    {
        public CandidateStatusViewModel Map(CandidateStatusDetails input)
        {
            var result = AutoMapper.Mapper.DynamicMap<CandidateStatusDetails, CandidateStatusViewModel>(input);
            return result;
        }
    }
}