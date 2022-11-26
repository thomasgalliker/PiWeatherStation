using System.Diagnostics;

namespace WeatherDisplay.Model
{
    [DebuggerDisplay("{this.Name}")]
    public class Place
    {
        public Place()
        {
        }

        public Place(string name, double latitude, double longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}