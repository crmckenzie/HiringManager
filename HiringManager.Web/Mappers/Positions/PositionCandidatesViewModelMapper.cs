using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiringManager.DomainServices;
using HiringManager.Mappers;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Mappers.Positions
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