using System.Collections.Generic;

namespace WeatherDisplay.Model
{
    public class TemperatureWeatherDisplayCompilationOptions
    {
        public TemperatureWeatherDisplayCompilationOptions()
        {
            this.Places = new List<Place>();
        }

        public ICollection<Place> Places { get; set; }
    }
}