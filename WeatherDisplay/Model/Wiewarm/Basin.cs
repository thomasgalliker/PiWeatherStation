using System;
using Newtonsoft.Json;
using UnitsNet;
using WeatherDisplay.Model.Wiewarm.Converters;

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
        [JsonConverter(typeof(TemperatureJsonConverter))]
        public Temperature Temperature { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("typ")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("smskeywords")]
        public string SmsKeywords { get; set; }

        [JsonProperty("smsname")]
        public string SmsName { get; set; }

        [JsonProperty("ismain")]
        public string IsMain { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}