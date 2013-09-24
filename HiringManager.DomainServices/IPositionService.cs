using System.Collections;

namespace HiringManager.DomainServices
{
    public interface IPositionService
    {
        QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request);
        PositionDetails Details(int id);
        CandidateStatusDetails GetCandidateStatusDetails(int candidateStatusId);
        
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        AddCandidateResponse AddCandidate(AddCandidateRequest request);

        CandidateStatusResponse SetCandidateStatus(int candidateStatusId, string status);
        CandidateStatusResponse Hire(int candidateStatusId);
    }
}