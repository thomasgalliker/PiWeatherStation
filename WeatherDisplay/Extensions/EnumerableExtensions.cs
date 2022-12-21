using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherDisplay.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static T GetNextIterative<T>(this ICollection<T> source, ref int current)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (++current >= source.Count)
            {
                current = 0;
            }

            var item = source.ElementAt(current);
            return item;
        }

    }
}
