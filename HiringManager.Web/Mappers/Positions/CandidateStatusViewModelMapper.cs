using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiringManager.DomainServices;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Mappers.Positions
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