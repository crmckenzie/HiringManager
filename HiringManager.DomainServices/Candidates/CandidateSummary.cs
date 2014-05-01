namespace HiringManager.DomainServices.Candidates
{
    public class CandidateSummary
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }

        public int? SourceId { get; set; }
        public string Source { get; set; }
    }
}