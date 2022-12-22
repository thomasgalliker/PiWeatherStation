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

        public static T GetNextElement<T>(this T[] array, T element, T defaultValue = default)
        {
            var elem = defaultValue;

            if (array.Length == 0)
            {
                return elem;
            }

            var index = Array.IndexOf(array, element);
            if (index < 0 && array.Length > 0)
            {
                return defaultValue;
            }

            if (array.Length > 1 && index >= 0 && index < array.Length - 1)
            {
                elem = array[index + 1];
            }
            else if (index == array.Length - 1)
            {
                elem = array[0];
            }

            return elem;
        }
    }
}
