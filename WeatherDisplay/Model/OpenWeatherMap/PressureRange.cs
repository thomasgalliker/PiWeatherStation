using System;
using System.Collections.Generic;
using System.Globalization;
using WeatherDisplay.Resources.Strings;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class PressureRange : Range<int>, IFormattable
    {
        public static readonly PressureRange VeryLow = new PressureRange(nameof(VeryLow), min: 0, max: 998, minInclusive: true, maxInclusive: true);
        public static readonly PressureRange Low = new PressureRange(nameof(Low), min: 998, max: 1008, minInclusive: false, maxInclusive: false);
        public static readonly PressureRange Average = new PressureRange(nameof(Average), min: 1008, max: 1018, minInclusive: true, maxInclusive: true);
        public static readonly PressureRange High = new PressureRange(nameof(High), min: 1018, max: 1028, minInclusive: false, maxInclusive: false);
        public static readonly PressureRange VeryHigh = new PressureRange(nameof(VeryHigh), min: 1028, max: int.MaxValue, minInclusive: true, maxInclusive: true);

        public static readonly IEnumerable<PressureRange> All = new List<PressureRange>
        {
            VeryLow, Low, Average, High, VeryHigh
        };

        private readonly string resourceId;

        private PressureRange(string resourceId, int min, int max, bool minInclusive, bool maxInclusive)
            : base(min, max, minInclusive, maxInclusive)
        {
            this.resourceId = resourceId;
        }

        public static PressureRange FromValue(int value)
        {
            foreach (var pressureRange in All)
            {
                if (pressureRange.InRange(value))
                {
                    return pressureRange;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 100");
        }

        public override string ToString()
        {
            return this.ToString("N", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "N";
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            switch (format)
            {
                case "I":
                    return base.ToString();
                case "N":
                default:
                    var str = PressureRanges.ResourceManager.GetString(this.resourceId, (CultureInfo)provider);
                    return str;
            }

        }
    }
}