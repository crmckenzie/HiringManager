
namespace HiringManager.EntityModel
{
    public class ContactInfo
    {
        public int? ContactInfoId { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Manager Manager { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
    }

}
