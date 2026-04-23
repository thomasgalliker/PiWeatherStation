using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
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

        [JsonPropertyName("badid")]
        public int Id { get; set; }

        [JsonPropertyName("badname")]
        public string Name { get; set; }

        [JsonPropertyName("kanton")]
        public string Canton { get; set; }

        [JsonPropertyName("plz")]
        public string ZipCode { get; set; }

        [JsonPropertyName("ort")]
        public string Place { get; set; }

        [JsonPropertyName("adresse1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("adresse2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefon")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("www")]
        public string WebsiteUrl { get; set; }

        [JsonPropertyName("long")]
        public int Longitude { get; set; }

        [JsonPropertyName("lat")]
        public int Latitude { get; set; }

        [JsonPropertyName("zeiten")]
        public string OpeningHours { get; set; }

        [JsonPropertyName("preise")]
        public string EntryFee { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("wetterort")]
        public string WeatherPlace { get; set; }

        [JsonPropertyName("uv_station_name")]
        public string UvStationName { get; set; }

        [JsonPropertyName("uv_wert")]
        public int UvIndex { get; set; }

        [JsonPropertyName("uv_date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime UvDate { get; set; }

        [JsonPropertyName("becken")]
        [JsonConverter(typeof(BasinCollectionJsonConverter))]
        public IReadOnlyCollection<Basin> Basins { get; set; }

        [JsonPropertyName("bilder")]
        public IReadOnlyCollection<object> Pictures { get; set; }

        [JsonPropertyName("wetter")]
        public IReadOnlyCollection<Weather> WeatherInfos { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
