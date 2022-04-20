using System;
using System.Globalization;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [Serializable]
    public struct Humidity : IComparable, IComparable<Humidity>, IComparable<int>, IEquatable<Humidity>, IFormattable
    {
        public Humidity(int value)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(string.Format($"{0} must be between 0 and 100", value));
            }

            this.Value = value;
            this.Range = HumidityRange.FromValue(value);
        }

        public int Value { get; private set; }
        
        public HumidityRange Range { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj is Humidity)
            {
                return this.Value.CompareTo((int)obj);
            }

            return this.Value.CompareTo(obj);
        }

        public int CompareTo(Humidity other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(int other)
        {
            return this.Value.CompareTo(other);
        }

        public bool Equals(Humidity other)
        {
            return this.Value.Equals(other.Value);
        }

        public static Humidity operator +(int v, Humidity h)
        {
            return new Humidity(h.Value + v);
        }

        public static Humidity operator +(Humidity h, int v)
        {
            return v + h;
        }

        public static Humidity operator +(Humidity h1, Humidity h2)
        {
            return h1 + h2.Value;
        }

        public static Humidity operator -(Humidity h, int v)
        {
            return new Humidity(h.Value - v);
        }

        public static Humidity operator -(Humidity h1, Humidity h2)
        {
            return h1 - h2.Value;
        }

        public static Humidity operator *(Humidity h, int v)
        {
            return new Humidity(h.Value * v);
        }

        public static Humidity operator /(Humidity h, int v)
        {
            return new Humidity(h.Value / v);
        }

        public static implicit operator Humidity(int v) => new Humidity(v);

        public static implicit operator Humidity(long v) => new Humidity((int)v);

        public static implicit operator int(Humidity h) => h.Value;
        
        public static implicit operator long(Humidity h) => h.Value;

        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "0";
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            return $"{this.Value.ToString(format, provider)}%";
        }
    }
}