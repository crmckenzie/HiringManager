using System.Linq;
using HiringManager.Domain;
using HiringManager.DomainServices;

namespace HiringManager.Mappers
{
    public class PositionSummaryMapper : IMapper<Position, PositionSummary>
    {
        public PositionSummary Map(Position input)
        {
            var result = AutoMapper.Mapper.DynamicMap<Position, PositionSummary>(input);

            var passedCandidates = input.Candidates.Where(row => row.Status == "Passed");
            var inReview = input.Candidates.Except(passedCandidates);
            result.CandidatesAwaitingReview = inReview.Count();

            return result;
        }
    }
}