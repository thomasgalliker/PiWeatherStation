using System;
using UnitsNet;

namespace WeatherDisplay
{
    public class TemperatureSet
    {
        public DateTime DateTime { get; set; }

        public Temperature Min { get; set; }

        public Temperature Avg { get; set; }

        public Temperature Max { get; set; }

        public double Rain { get; set; }

        public override string ToString()
        {
            return $"{this.DateTime} {this.Min:0} - {this.Avg:0} - {this.Max:0}";
        }
    }
}