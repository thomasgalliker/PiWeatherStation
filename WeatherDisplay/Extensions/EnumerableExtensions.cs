using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

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

        public static T GetNextElement<T>(this IList<T> list, T element, T defaultValue = default)
        {
            var next = defaultValue;

            if (list.Count == 0)
            {
                return next;
            }

            var index = list.IndexOf(element);
            if (index < 0 && list.Count > 0)
            {
                return defaultValue;
            }

            if (list.Count > 1 && index >= 0 && index < list.Count - 1)
            {
                next = list[index + 1];
            }
            else if (index == list.Count - 1)
            {
                next = list[0];
            }

            return next;
        }
    }
}
