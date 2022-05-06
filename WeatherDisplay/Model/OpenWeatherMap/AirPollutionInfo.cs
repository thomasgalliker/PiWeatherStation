using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class AirPollutionInfo
    {
        public AirPollutionInfo()
        {
            this.Items = new List<AirPollutionInfoItem>();
        }

        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("list")]
        public IReadOnlyCollection<AirPollutionInfoItem> Items { get; set; }
    }
}