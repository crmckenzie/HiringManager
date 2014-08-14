namespace HiringManager.DomainServices.Candidates
{
    public class QueryNotesRequest
    {
        public int[] CandidateIds { get; set; }
        public int[] PositionIds { get; set; }
        public int[] CandidateStatusIds { get; set; }
    }
}