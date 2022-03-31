using System;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    /// <summary>
    /// Sources:
    /// https://raw.githubusercontent.com/ImaMapleTree/MirenFalls/0a482e153d3353d45608cd936a02445ddcdf023c/MirenFalls/Internal/Utils/Range.cs
    /// https://github.com/dundich/Pay/blob/f06645ce1c57c4be33f783944965d3faaaad6b88/Libs/Maybe2/IRange.cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T> where T : IComparable
    {
        private readonly bool minInclusive;
        private readonly bool maxInclusive;

        public T Min { get; set; }

        public T Max { get; set; }

        public Range()
        {
        }

        public Range(T min, T max, bool minInclusive = true, bool maxInclusive = true)
        {
            this.Min = min;
            this.Max = max;
            this.minInclusive = minInclusive;
            this.maxInclusive = maxInclusive;
        }

        public bool InRange(T value)
        {
            var isGreaterThanMin = this.minInclusive ? value.CompareTo(this.Min) >= 0 : value.CompareTo(this.Min) > 0;
            var isLessThanMax = this.maxInclusive ? value.CompareTo(this.Max) <= 0 : value.CompareTo(this.Max) < 0;

            return (isGreaterThanMin && isLessThanMax);
        }

        public bool IsEmpty
        {
            get
            {
                if (typeof(T) is IComparable)
                {
                    return this.Min.CompareTo(this.Max) >= 0;
                }
                else
                {
                    return this.Min.Equals(this.Max);
                }
            }
        }

        public override string ToString()
        {
            var start = this.minInclusive ? "[" : "(";
            var end = this.maxInclusive ? "]" : ")";
            return start + this.Min + ", " + this.Max + end;
        }
    }
}