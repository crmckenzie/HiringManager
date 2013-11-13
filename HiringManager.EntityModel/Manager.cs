using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiringManager.EntityModel
{
    public class Manager
    {
        public int? ManagerId { get; set; }

        public virtual IList<Position> Positions { get; set; }
 
        public virtual IList<ContactInfo> ContactInfo { get; set; } 

        [StringLength(250)]
        public string Name { get; set; }
        
        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }

    }
}