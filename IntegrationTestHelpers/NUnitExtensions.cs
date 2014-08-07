using System;
using NUnit.Framework.Constraints;

namespace IntegrationTestHelpers
{
    public static class NUnitExtensions
    {
        public static CollectionItemsEqualConstraint Using<T>(this CollectionEquivalentConstraint constraint,
            Func<T, T, bool> expression)
        {
            return constraint.Using(new PredicateComparison<T>(expression));
        }

        public static CollectionItemsEqualConstraint Using<TExpected, TActual>(this CollectionEquivalentConstraint constraint,
            Func<TExpected, TActual, bool> expression)
        {
            return constraint.Using(new PredicateComparison<TExpected, TActual>(expression));
        }
    }
}