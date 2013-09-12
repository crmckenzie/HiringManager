using System.Collections.Generic;

namespace HiringManager.Domain
{
    public class HiringManager
    {
        public int? HiringManagerId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public IList<Position> Positions { get; set; }
 
        public IList<ContactInfo> ContactInfo { get; set; } 

        public IList<Message> Messages { get; set; } 
    }
}