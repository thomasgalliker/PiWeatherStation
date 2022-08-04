using Newtonsoft.Json;

namespace WeatherDisplay.Model.Wiewarm
{
    public class Becken
    {
        [JsonProperty("beckenid")]
        public int Id { get; set; }

        [JsonProperty("beckenname")]
        public string Name { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("typ")]
        public string Typ { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("smskeywords")]
        public string Smskeywords { get; set; }

        [JsonProperty("smsname")]
        public string Smsname { get; set; }

        [JsonProperty("ismain")]
        public string Ismain { get; set; }

        [JsonProperty("date_pretty")]
        public string DatePretty { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}