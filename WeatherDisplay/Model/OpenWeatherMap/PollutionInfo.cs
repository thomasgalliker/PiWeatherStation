using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class PollutionInfo
    {
        public PollutionInfo()
        {
            this.Items = new List<PollutionInfoItem>();
        }

        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("list")]
        public IReadOnlyCollection<PollutionInfoItem> Items { get; set; }
    }
}