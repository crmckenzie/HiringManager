
using System.ComponentModel.DataAnnotations;

namespace HiringManager.EntityModel
{
    public class ContactInfo
    {
        public int? ContactInfoId { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Manager Manager { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string Value { get; set; }
    }

}
