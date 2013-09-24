namespace HiringManager.DomainServices
{
    public class QueryPositionSummariesRequest
    {
        public string[] Statuses { get; set; }
        public int[] ManagerIds { get; set; }
    }
}