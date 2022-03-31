using System;
using System.Collections.Generic;
using System.Globalization;
using WeatherDisplay.Resources.Strings;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class HumidityRange : Range<int>, IFormattable
    {
        public static readonly HumidityRange VeryDry = new HumidityRange(nameof(VeryDry), min: 0, max: 30, minInclusive: true, maxInclusive: true);
        public static readonly HumidityRange Dry = new HumidityRange(nameof(Dry), min: 30, max: 40, minInclusive: false, maxInclusive: false);
        public static readonly HumidityRange Average = new HumidityRange(nameof(Average), min: 40, max: 70, minInclusive: true, maxInclusive: true);
        public static readonly HumidityRange Moist = new HumidityRange(nameof(Moist), min: 70, max: 80, minInclusive: false, maxInclusive: false);
        public static readonly HumidityRange VeryMoist = new HumidityRange(nameof(VeryMoist), min: 80, max: 100, minInclusive: true, maxInclusive: true);

        private static readonly IEnumerable<HumidityRange> All = new List<HumidityRange>
        {
            VeryDry, Dry, Average, Moist, VeryMoist
        };

        private readonly string resourceId;

        private HumidityRange(string resourceId, int min, int max, bool minInclusive, bool maxInclusive)
            : base(min, max, minInclusive, maxInclusive)
        {
            this.resourceId = resourceId;
        }

        public static HumidityRange FromValue(int value)
        {
            foreach (var humidityRange in All)
            {
                if (humidityRange.InRange(value))
                {
                    return humidityRange;
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
                    var str = HumidityRanges.ResourceManager.GetString(this.resourceId, (CultureInfo)provider);
                    return str;
            }

        }
    }
}