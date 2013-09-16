namespace HiringManager.Web.ApplicationServices.Positions
{
    public interface IPositionApplicationService
    {
        QueryResponse<PositionSummary> GetOpenPositions();
    }
}