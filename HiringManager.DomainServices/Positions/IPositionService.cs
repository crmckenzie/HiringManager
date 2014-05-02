namespace HiringManager.DomainServices.Positions
{
    public interface IPositionService
    {
        NewCandidateResponse AddCandidate(NewCandidateRequest request);
        IValidatedResponse Close(int positionId);
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        PositionDetails Details(int id);
        CandidateStatusDetails GetCandidateStatusDetails(int candidateStatusId);
        CandidateStatusResponse Hire(int candidateStatusId);
        QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request);
        CandidateStatusResponse SetCandidateStatus(int candidateStatusId, string status); 
    }
}