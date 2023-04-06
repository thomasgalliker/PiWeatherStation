using System.Collections.Generic;
using WeatherDisplay.Model.Settings;

namespace WeatherDisplay.Pages.OpenWeatherMap
{
    public class OpenWeatherMapPageOptions
    {
        public OpenWeatherMapPageOptions()
        {
            this.Places = new List<Place>();
        }

        public ICollection<Place> Places { get; set; }
    }
}