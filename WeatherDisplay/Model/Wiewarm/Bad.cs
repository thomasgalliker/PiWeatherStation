using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherDisplay.Model.Wiewarm.Converters;

namespace WeatherDisplay.Model.Wiewarm
{
    /// <summary>
    /// The representation of a bath (German: "Bad").
    /// </summary>
    public class Bath
    {
        public Bath()
        {
            this.Basins = new List<Basin>();
            this.Pictures = new List<object>();
            this.WeatherInfos = new List<Weather>();
        }

        [JsonProperty("badid")]
        public int Id { get; set; }

        [JsonProperty("badname")]
        public string Name { get; set; }

        [JsonProperty("kanton")]
        public string Canton { get; set; }

        [JsonProperty("plz")]
        public string ZipCode { get; set; }

        [JsonProperty("ort")]
        public string Place { get; set; }

        [JsonProperty("adresse1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("adresse2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefon")]
        public string PhoneNumber { get; set; }

        [JsonProperty("www")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("long")]
        public int Longitude { get; set; }

        [JsonProperty("lat")]
        public int Latitude { get; set; }

        [JsonProperty("zeiten")]
        public string OpeningHours { get; set; }

        [JsonProperty("preise")]
        public string EntryFee { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("wetterort")]
        public string WeatherPlace { get; set; }

        [JsonProperty("uv_station_name")]
        public string UvStationName { get; set; }

        [JsonProperty("uv_wert")]
        public int UvIndex { get; set; }

        [JsonProperty("uv_date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime UvDate { get; set; }

        [JsonProperty("becken")]
        [JsonConverter(typeof(BeckenJsonConverter))]
        public IReadOnlyCollection<Basin> Basins { get; set; }

        [JsonProperty("bilder")]
        public IReadOnlyCollection<object> Pictures { get; set; }

        [JsonProperty("wetter")]
        public IReadOnlyCollection<Weather> WeatherInfos { get; set; }
    }
}