namespace HiringManager.DomainServices.Positions
{
    public class AddCandidateResponse : ValidatedResponse
    {
        public int CandidateId { get; set; }
        public int CandidateStatusId { get; set; }
        public int PositionId { get; set; }
    }
}