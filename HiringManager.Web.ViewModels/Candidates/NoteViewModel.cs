using System;

namespace HiringManager.Web.ViewModels.Candidates
{
    public class NoteViewModel
    {
        public int AuthorId { get; set; }
        public DateTime Authored { get; set; }
        public string Author { get; set; }
        public int CandidateId { get; set; }
        public string Candidate { get; set; }
        public int NoteId { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public string Text { get; set; }
    }
}