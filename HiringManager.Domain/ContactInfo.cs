using System.Linq;
using System.Text;

namespace HiringManager.Domain
{
    public class ContactInfo
    {
        public int? ContactInfoId { get; set; }

        public Candidate Candidate { get; set; }
        public HiringManager HiringManager { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
    }

}
