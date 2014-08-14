using System;

namespace HiringManager.DomainServices.Candidates
{
    public class NoteDetails
    {
        public int NoteId { get; set; }
        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }
        public string Candidate { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public DateTime Authored { get; set; }
    }
}