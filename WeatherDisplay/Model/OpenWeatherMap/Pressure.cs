using System;
using System.Globalization;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [Serializable]
    public struct Pressure : IComparable, IComparable<Pressure>, IComparable<int>, IEquatable<Pressure>, IFormattable
    {
        public Pressure(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(string.Format($"{0} must be greather than 0", value));
            }

            this.Value = value;
            this.Range = PressureRange.FromValue(value);
        }

        public int Value { get; private set; }
        
        public PressureRange Range { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj is Pressure)
            {
                return this.Value.CompareTo((int)obj);
            }

            return this.Value.CompareTo(obj);
        }

        public int CompareTo(Pressure other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(int other)
        {
            return this.Value.CompareTo(other);
        }

        public bool Equals(Pressure other)
        {
            return this.Value.Equals(other.Value);
        }

        public static Pressure operator +(int v, Pressure p)
        {
            return new Pressure(p.Value + v);
        }

        public static Pressure operator +(Pressure p, int v)
        {
            return v + p;
        }

        public static Pressure operator +(Pressure p1, Pressure p2)
        {
            return p1 + p2.Value;
        }

        public static Pressure operator -(Pressure p, int v)
        {
            return new Pressure(p.Value - v);
        }

        public static Pressure operator -(Pressure p1, Pressure p2)
        {
            return p1 - p2.Value;
        }

        public static Pressure operator *(Pressure p, int v)
        {
            return new Pressure(p.Value * v);
        }

        public static Pressure operator /(Pressure p, int v)
        {
            return new Pressure(p.Value / v);
        }

        public static implicit operator Pressure(int v) => new Pressure(v);

        public static implicit operator Pressure(long v) => new Pressure((int)v);

        public static implicit operator int(Pressure p) => p.Value;
        
        public static implicit operator long(Pressure p) => p.Value;

        public override string ToString()
        {
            return this.ToString(null, CultureInfo.CurrentCulture);
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

            return $"{this.Value.ToString(format, provider)}hPa";
        }
    }
}