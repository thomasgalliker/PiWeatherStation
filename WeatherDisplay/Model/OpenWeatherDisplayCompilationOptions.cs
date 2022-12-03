using System.Collections.Generic;

namespace WeatherDisplay.Model
{
    public class OpenWeatherDisplayCompilationOptions
    {
        public OpenWeatherDisplayCompilationOptions()
        {
            this.Places = new List<Place>();
        }

        public ICollection<Place> Places { get; set; }
    }
}