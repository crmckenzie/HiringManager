namespace HiringManager.DomainServices
{
    public interface IPositionService
    {
        QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request);
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        HireCandidateResponse Hire(HireCandidateRequest request);
    }
}