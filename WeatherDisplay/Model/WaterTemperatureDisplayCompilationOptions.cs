using System.Collections.Generic;

namespace WeatherDisplay.Model
{
    public class WaterTemperatureDisplayCompilationOptions
    {
        public WaterTemperatureDisplayCompilationOptions()
        {
            this.Places = new List<string>();
        }

        public ICollection<string> Places { get; set; }
    }
}