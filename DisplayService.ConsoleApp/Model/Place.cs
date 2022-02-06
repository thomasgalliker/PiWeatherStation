namespace DisplayService.ConsoleApp
{
    internal class Place
    {
        public Place(string name, double latitude, double longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public string Name { get; }

        public double Latitude { get; }

        public double Longitude { get; }
    }
}