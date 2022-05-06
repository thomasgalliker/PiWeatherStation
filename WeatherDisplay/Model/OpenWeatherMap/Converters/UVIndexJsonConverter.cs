using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal class UVIndexJsonConverter : JsonConverter<UVIndex>
    {
        public override void WriteJson(JsonWriter writer, UVIndex value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }

        public override UVIndex ReadJson(JsonReader reader, Type objectType, UVIndex existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is double uvIndex)
            {
                return (UVIndex)uvIndex;
            }

            if (reader.Value is long uvIndexLong)
            {
                return (UVIndex)uvIndexLong;
            }

            throw new NotSupportedException($"Cannot convert from {reader.Value} to UVIndex");
        }
    }
}
