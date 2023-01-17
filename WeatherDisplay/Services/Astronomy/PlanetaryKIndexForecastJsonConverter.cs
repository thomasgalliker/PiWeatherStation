using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherDisplay.Services.Astronomy
{
    public class PlanetaryKIndexForecastJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PlanetaryKIndexForecast[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var table = JArray.Load(reader);
            var items = new PlanetaryKIndexForecast[table.Count - 1];
            for (var i = 1; i < table.Count; i++)
            {
                var row = (JArray)table[i];
                items[i - 1] = new PlanetaryKIndexForecast
                {
                    TimeTag = DateTime.SpecifyKind(row[0].Value<DateTime>(), DateTimeKind.Utc),
                    KpIndex = (decimal)row[1],
                    Observed = (string)row[2],
                    NoaaScale = (string)row[3],
                };
            }
            return items;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}