using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Pure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> DistinctConcat<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            HashSet<T> returned = new HashSet<T>();
            foreach (T element in first)
            {
                if (returned.Add(element))
                {
                    yield return element;
                }
            }
            foreach (T element in second)
            {
                if (returned.Add(element))
                {
                    yield return element;
                }
            }
        }
    }
}
