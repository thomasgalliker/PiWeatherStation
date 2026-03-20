using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherDisplay.Services.Astronomy
{
    public class PlanetaryKIndexForecastJsonConverter : JsonConverter<PlanetaryKIndexForecast[]>
    {
        public override PlanetaryKIndexForecast[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var table = document.RootElement;
            var items = new PlanetaryKIndexForecast[table.GetArrayLength() - 1];

            for (var i = 1; i < table.GetArrayLength(); i++)
            {
                var row = table[i];
                items[i - 1] = new PlanetaryKIndexForecast
                {
                    TimeTag = DateTime.SpecifyKind(row[0].GetDateTime(), DateTimeKind.Utc),
                    KpIndex = row[1].GetDecimal(),
                    Observed = row[2].GetString(),
                    NoaaScale = row[3].GetString(),
                };
            }

            return items;
        }

        public override void Write(Utf8JsonWriter writer, PlanetaryKIndexForecast[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
