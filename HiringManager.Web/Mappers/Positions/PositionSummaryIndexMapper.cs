using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiringManager.DomainServices;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Mappers.Positions
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