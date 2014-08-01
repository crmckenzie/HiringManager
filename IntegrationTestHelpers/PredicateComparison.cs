using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegrationTestHelpers
{
    public class PredicateComparison<T> : IComparer<T>
    {
        private readonly Func<T, T, bool> _comparison;

        public int Compare(T x, T y)
        {
            if (_comparison.Invoke(x, y))
            {
                return 0;
            }
            return -1;
        }

        public PredicateComparison(Func<T, T, bool> comparison)
        {
            _comparison = comparison;
        }
    }

    public class PredicateComparison<TExpected, TActual> : IComparer
    {
        private readonly Func<TExpected, TActual, bool> _func;

        public int Compare(object x, object y)
        {
            var expected = (TExpected)x;
            var actual = (TActual)y;

            if (_func(expected, actual))
                return 0;

            return -1;
        }

        public PredicateComparison(Func<TExpected, TActual, bool> func)
        {
            _func = func;
        }
    }
}