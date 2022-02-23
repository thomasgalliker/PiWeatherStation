namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class WeatherResponse
    {
        public float Temperature { get; set; }

        public string UnitSystem { get; set; }

        public string LocationName { get; set; }

        public int? ConditionId { get; set; }

        public string IconUrl { get; set; }

    }
}