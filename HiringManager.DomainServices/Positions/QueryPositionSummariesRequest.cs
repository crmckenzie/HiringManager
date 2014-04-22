namespace HiringManager.DomainServices.Positions
{
    public class QueryPositionSummariesRequest
    {
        public string[] Statuses { get; set; }
        public int[] ManagerIds { get; set; }
    }
}