namespace HiringManager.DomainServices.Candidates
{
    public interface ICandidateService
    {
        CandidateDetails Get(int id);
        ValidatedResponse Save(SaveCandidateRequest request);
        QueryResponse<CandidateSummary> Query(QueryCandidatesRequest request);
    }
}
