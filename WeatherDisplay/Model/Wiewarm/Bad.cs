using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherDisplay.Model.Wiewarm.Converters;

namespace WeatherDisplay.Model.Wiewarm
{
    public class Bad
    {
        public Bad()
        {
            this.Becken = new List<Becken>();
            this.Bilder = new List<object>();
            this.Wetter = new List<Wetter>();
        }

        [JsonProperty("badid")]
        public int Id { get; set; }

        [JsonProperty("badname")]
        public string Name { get; set; }

        [JsonProperty("kanton")]
        public string Kanton { get; set; }

        [JsonProperty("plz")]
        public string Plz { get; set; }

        [JsonProperty("ort")]
        public string Ort { get; set; }

        [JsonProperty("adresse1")]
        public string Adresse1 { get; set; }

        [JsonProperty("adresse2")]
        public string Adresse2 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefon")]
        public string Telefon { get; set; }

        [JsonProperty("www")]
        public string Www { get; set; }

        [JsonProperty("long")]
        public int Long { get; set; }

        [JsonProperty("lat")]
        public int Lat { get; set; }

        [JsonProperty("zeiten")]
        public string Zeiten { get; set; }

        [JsonProperty("preise")]
        public string Preise { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("wetterort")]
        public string Wetterort { get; set; }

        [JsonProperty("uv_station_name")]
        public string UvStationName { get; set; }

        [JsonProperty("uv_wert")]
        public int UvWert { get; set; }

        [JsonProperty("uv_date")]
        public string UvDate { get; set; }

        [JsonProperty("uv_date_pretty")]
        public string UvDatePretty { get; set; }

        [JsonProperty("becken")]
        [JsonConverter(typeof(BeckenJsonConverter))]
        public IReadOnlyCollection<Becken> Becken { get; set; }

        [JsonProperty("bilder")]
        public IReadOnlyCollection<object> Bilder { get; set; }

        [JsonProperty("wetter")]
        public IReadOnlyCollection<Wetter> Wetter { get; set; }
    }
}