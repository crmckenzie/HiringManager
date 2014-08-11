using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiringManager.EntityModel
{
    public class Manager
    {
        public Manager()
        {
            this.Positions = new List<Position>();
            this.ContactInfo = new List<ContactInfo>();
            this.Notes = new List<Note>();
        }

        public int? ManagerId { get; set; }

        public virtual IList<Position> Positions { get; set; }

        public virtual IList<ContactInfo> ContactInfo { get; set; }

        public virtual IList<Note> Notes { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }
    }
}