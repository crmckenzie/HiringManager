namespace HiringManager.DomainServices.Sources
{
    public interface ISourceService
    {
        QueryResponse<SourceSummary> Query(QuerySourcesRequest request);
    }
}
