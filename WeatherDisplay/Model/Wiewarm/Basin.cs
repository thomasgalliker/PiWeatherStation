using System;
using System.Text.Json.Serialization;
using UnitsNet;
using WeatherDisplay.Model.Wiewarm.Converters;

namespace WeatherDisplay.Model.Wiewarm
{
    /// <summary>
    /// The representation of a basin or pool within a bath (German: "Becken").
    /// </summary>
    public class Basin
    {
        [JsonPropertyName("beckenid")]
        public int Id { get; set; }

        [JsonPropertyName("beckenname")]
        public string Name { get; set; }

        [JsonPropertyName("temp")]
        [JsonConverter(typeof(TemperatureJsonConverter))]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime Date { get; set; }

        [JsonPropertyName("typ")]
        public string Type { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("smskeywords")]
        public string SmsKeywords { get; set; }

        [JsonPropertyName("smsname")]
        public string SmsName { get; set; }

        [JsonPropertyName("ismain")]
        public string IsMain { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
