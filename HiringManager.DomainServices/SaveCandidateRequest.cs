namespace HiringManager.DomainServices
{
    public class SaveCandidateRequest
    {
        public int? CandidateId { get; set; }
        public int? SourceId { get; set; }

        public string Name { get; set; }

        public ContactInfoDetails[] ContactInfo { get; set; }
    }
}