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
            var root = document.RootElement;

            if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
            {
                return Array.Empty<PlanetaryKIndexForecast>();
            }

            var firstItem = root[0];

            if (firstItem.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException($"Unsupported planetary K-index forecast payload shape: {firstItem.ValueKind}.");
            }

            var items = new PlanetaryKIndexForecast[root.GetArrayLength()];

            for (var i = 0; i < root.GetArrayLength(); i++)
            {
                var row = root[i];
                items[i] = new PlanetaryKIndexForecast
                {
                    TimeTag = DateTime.SpecifyKind(row.GetProperty("time_tag").GetDateTime(), DateTimeKind.Utc),
                    KpIndex = row.GetProperty("kp").GetDecimal(),
                    Observed = row.GetProperty("observed").GetString(),
                    NoaaScale = row.TryGetProperty("noaa_scale", out var noaaScale) && noaaScale.ValueKind != JsonValueKind.Null
                        ? noaaScale.GetString()
                        : null,
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
