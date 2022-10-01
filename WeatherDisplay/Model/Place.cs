namespace WeatherDisplay.Model
{
    public class Place
    {
        public Place()
        {
        }

        public Place(string name, int plz, double latitude, double longitude)
        {
            this.Name = name;
            this.Plz = plz;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public string Name { get; set; }

        public int Plz { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}