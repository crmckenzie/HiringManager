using System.Collections.Generic;

namespace HiringManager.EntityModel
{
    public class Message
    {
        public int? MessageId { get; set; }

        public virtual Manager Manager { get; set; }
        public virtual Candidate Candidate { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public virtual IList<Document> Attachments { get; set; } 
    }
}