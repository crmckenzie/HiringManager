using HiringManager.DomainServices;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Positions;

namespace HiringManager.Mappers.Presentation.Positions
{
    public class PositionSummaryIndexMapper: IMapper<QueryResponse<PositionSummary>, IndexViewModel<PositionSummaryIndexItem>>
    {
        public IndexViewModel<PositionSummaryIndexItem> Map(QueryResponse<PositionSummary> input)
        {
            var indexViewModel = new IndexViewModel<PositionSummaryIndexItem>()
                                 {
                                     Data =
                                     {
                           
                                     }
                                 };

            foreach (var positionSummary in input.Data)
            {
                var indexItem = AutoMapper.Mapper.DynamicMap<PositionSummary, PositionSummaryIndexItem>(positionSummary);

                indexViewModel.Data.Add(indexItem);
            }

            return indexViewModel;
        }
    }
}