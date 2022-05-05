using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal class AirQualityJsonConverter : JsonConverter<AirQuality>
    {
        public override void WriteJson(JsonWriter writer, AirQuality value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }

        public override AirQuality ReadJson(JsonReader reader, Type objectType, AirQuality existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is long celsiusLong)
            {
                return AirQuality.FromValue((int)celsiusLong);
            }

            throw new NotSupportedException($"Cannot convert from {reader.Value} to AirQuality");
        }
    }
}
