using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.Wiewarm
{
    /// <summary>
    /// The representation of a basin or pool within a bath (German: "Becken").
    /// </summary>
    public class Basin
    {
        [JsonProperty("beckenid")]
        public int Id { get; set; }

        [JsonProperty("beckenname")]
        public string Name { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("typ")]
        public string Typ { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("smskeywords")]
        public string Smskeywords { get; set; }

        [JsonProperty("smsname")]
        public string Smsname { get; set; }

        [JsonProperty("ismain")]
        public string IsMain { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}