using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringManager.EntityModel
{
    public class Opening
    {
        public int? OpeningId { get; set; }

        public int? PositionId { get; set; }
        public Position Position { get; set; }

        public int? FilledById { get; set; }

        [ForeignKey("FilledById")]
        public virtual Candidate FilledBy { get; set; }

        public DateTime? FilledDate { get; set; }

        public string Status { get; set; }

        public bool IsFilled()
        {
            return this.FilledBy != null;
        }
    }
}
