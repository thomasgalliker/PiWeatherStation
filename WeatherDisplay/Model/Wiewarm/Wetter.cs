using Newtonsoft.Json;

namespace WeatherDisplay.Model.Wiewarm
{
    public class Wetter
    {
        [JsonProperty("wetter_symbol")]
        public int WetterSymbol { get; set; }

        [JsonProperty("wetter_temp")]
        public string WetterTemp { get; set; }

        [JsonProperty("wetter_date")]
        public string WetterDate { get; set; }

        [JsonProperty("wetter_date_pretty")]
        public string WetterDatePretty { get; set; }
    }
}