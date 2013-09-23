namespace HiringManager.DomainServices
{
    public class SetCandidateStatusRequest
    {
        public int CandidateStatusId { get; set; }
        public string Status { get; set; }
    }
}