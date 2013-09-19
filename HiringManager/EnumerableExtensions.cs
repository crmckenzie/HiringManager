using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public static class EnumerableExtensions
    {
        public static bool SafeAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }
    }
}
