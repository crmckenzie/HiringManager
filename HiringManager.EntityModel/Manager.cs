using System.Collections.Generic;

namespace HiringManager.EntityModel
{
    public class Manager
    {
        public int? ManagerId { get; set; }

        public virtual IList<Position> Positions { get; set; }
 
        public virtual IList<ContactInfo> ContactInfo { get; set; } 

        public virtual IList<Message> Messages { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }

    }
}