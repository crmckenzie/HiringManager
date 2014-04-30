namespace HiringManager.DomainServices.Candidates
{
    public class CandidateDetails
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }

        public ContactInfoDetails[] ContactInfo { get; set; }

        public int? SourceId { get; set; }
        public string Source { get; set; }
    }
}