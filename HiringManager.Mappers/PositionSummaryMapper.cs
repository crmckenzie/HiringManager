using System.Linq;
using HiringManager.DomainServices;
using HiringManager.EntityModel;

namespace HiringManager.Mappers
{
    public class PositionSummaryMapper : IMapper<Position, PositionSummary>
    {
        public PositionSummary Map(Position input)
        {
            var result = AutoMapper.Mapper.DynamicMap<Position, PositionSummary>(input);

            if (result.Status == "Filled") return result;
            
            var passedCandidates = input.Candidates.Where(row => row.Status == "Passed" || row.Status == "Hired");
            var inReview = input.Candidates.Except(passedCandidates);
            result.CandidatesAwaitingReview = inReview.Count();

            return result;
        }
    }
}