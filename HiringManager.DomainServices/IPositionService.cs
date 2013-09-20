using System.Collections;

namespace HiringManager.DomainServices
{
    public interface IPositionService
    {
        QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request);
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        AddCandidateResponse AddCandidate(AddCandidateRequest request);
        HireCandidateResponse Hire(HireCandidateRequest request);
        PositionDetails Details(int id);
    }
}