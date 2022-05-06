using System;
using System.Globalization;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [Serializable]
    public struct UVIndex : IComparable, IComparable<UVIndex>, IComparable<double>, IEquatable<UVIndex>, IFormattable
    {
        public UVIndex(double value)
        {
            if (value < UVIndexRange.MinValue || value > UVIndexRange.MaxValue)
            {
                throw new ArgumentOutOfRangeException(string.Format($"{0} must be between {UVIndexRange.MinValue} and {UVIndexRange.MaxValue}", value));
            }

            this.Value = value;
            this.Range = UVIndexRange.FromValue(value);
        }

        public double Value { get; private set; }

        public UVIndexRange Range { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj is UVIndex)
            {
                return this.Value.CompareTo((double)obj);
            }

            return this.Value.CompareTo(obj);
        }

        public int CompareTo(UVIndex other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(double other)
        {
            return this.Value.CompareTo(other);
        }

        public bool Equals(UVIndex other)
        {
            return this.Value.Equals(other.Value);
        }

        public static UVIndex operator +(double v, UVIndex uvi)
        {
            return new UVIndex(uvi.Value + v);
        }

        public static UVIndex operator +(UVIndex uvi, double v)
        {
            return v + uvi;
        }

        public static UVIndex operator +(UVIndex uvi1, UVIndex uvi2)
        {
            return uvi1 + uvi2.Value;
        }

        public static UVIndex operator -(UVIndex uvi, double v)
        {
            return new UVIndex(uvi.Value - v);
        }

        public static UVIndex operator -(UVIndex uvi1, UVIndex uvi2)
        {
            return uvi1 - uvi2.Value;
        }

        public static UVIndex operator *(UVIndex uvi, double v)
        {
            return new UVIndex(uvi.Value * v);
        }

        public static UVIndex operator /(UVIndex uvi, double v)
        {
            return new UVIndex(uvi.Value / v);
        }

        public static implicit operator UVIndex(double v) => new UVIndex(v);

        public static implicit operator double(UVIndex uvi) => uvi.Value;

        public override string ToString()
        {
            return this.ToString("0.#", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "0.0#";
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            return $"{this.Value.ToString(format, provider)}";
        }
    }
}