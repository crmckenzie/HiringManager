using System.Collections;

namespace HiringManager.DomainServices
{
    public interface IPositionService
    {
        QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request);
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        AddCandidateResponse AddCandidate(AddCandidateRequest request);
        CandidateStatusResponse Hire(HireCandidateRequest request);
        PositionDetails Details(int id);
        void SetCandidateStatus(int candidateStatusId, string status);
        CandidateStatusDetails GetCandidateStatusDetails(int candidateStatusId);
    }
}