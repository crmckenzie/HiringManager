
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;

namespace HiringManager.EntityModel
{
    public class CandidateStatus
    {
        public int? CandidateStatusId { get; set; }

        [Index("UQ_CandidateStatus", IsUnique = true, Order = 2)]
        public int? CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [Index("UQ_CandidateStatus", IsUnique = true, Order = 1)]
        public int? PositionId { get; set; }

        public virtual Position Position { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public bool IsHired()
        {
            return this.Position.Openings.Any(row => row.FilledBy == this.Candidate);
        }

        public bool CanHire()
        {
            if (this.Position.IsFilled())
                return false;

            if (this.Position.Openings.Any(row => row.FilledBy == this.Candidate))
            {
                return false;
            }

            return true;
        }

        public bool CanPass()
        {
            if (Position.IsFilled())
            {
                return false;
            }
            if (IsHired())
            {
                return false;
            }
            return this.Status != "Passed";
        }

        public bool CanSetStatus()
        {
            return !Position.IsFilled() && this.CanHire() && this.CanPass();
        }
    }
}
