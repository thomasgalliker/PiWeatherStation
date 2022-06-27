using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class WeatherForecast
    {
        public WeatherForecast()
        {
            this.Items = new List<WeatherForecastItem>();
        }

        [JsonProperty("cod")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public IReadOnlyCollection<WeatherForecastItem> Items { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

        public override string ToString()
        {
            var orderedItems = this.Items.OrderBy(i => i.DateTime);
            return $"From: {orderedItems.First().DateTime}, To: {orderedItems.Last().DateTime}";
        }
    }
}
