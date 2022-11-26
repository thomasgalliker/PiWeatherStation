using System.Collections.Generic;

namespace WeatherDisplay.Model
{
    public class MeteoSwissWeatherDisplayCompilationOptions
    {
        public MeteoSwissWeatherDisplayCompilationOptions()
        {
            this.Places = new List<Place>();
        }

        public ICollection<Place> Places { get; set; }
    }
}