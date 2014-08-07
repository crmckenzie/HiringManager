namespace HiringManager.DomainServices.Positions
{
    public interface IPositionService
    {
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        PositionDetails Details(int id);
        IValidatedResponse Close(int positionId);

        NewCandidateResponse AddNewCandidate(NewCandidateRequest request);
        AddCandidateResponse AddCandidate(AddCandidateRequest request);

        CandidateStatusDetails GetCandidateStatusDetails(int candidateStatusId);
        CandidateStatusResponse Hire(int candidateStatusId);
        QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request);
        CandidateStatusResponse SetCandidateStatus(int candidateStatusId, string status);
    }
}