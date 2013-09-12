using System.Collections.Generic;

namespace HiringManager.Domain
{
    public class Message
    {
        public int? MessageId { get; set; }

        public HiringManager HiringManager { get; set; }
        public Candidate Candidate { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public IList<Document> Attachments { get; set; } 
    }
}