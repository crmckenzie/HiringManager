using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Isg.Specification;
using HiringManager;

namespace HiringManager.Domain.Specifications
{
    public class PositionSpecification : CompositeSpecification<Position>
    {

        public string[] Statuses { get; set; }
        public int[] ManagerIds { get; set; }

        protected override IEnumerable<Expression<Func<Position, bool>>> GetExpressions()
        {
            if (Statuses.SafeAny())
            {
                yield return row => Statuses.Contains(row.Status);
            }

            if (ManagerIds.SafeAny())
            {
                yield return row => ManagerIds.Contains(row.CreatedById);
            }
        }
    }
}
