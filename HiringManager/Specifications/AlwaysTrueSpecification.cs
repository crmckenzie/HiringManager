using System;
using System.Linq.Expressions;
using Isg.Specification;

namespace HiringManager.Specifications
{
    public class AlwaysTrueSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> IsSatisfied()
        {
            return f => true;
        }
    }
}