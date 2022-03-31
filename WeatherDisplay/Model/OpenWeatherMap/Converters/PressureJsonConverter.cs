using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal class PressureJsonConverter : JsonConverter<Pressure>
    {
        public override void WriteJson(JsonWriter writer, Pressure value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }

        public override Pressure ReadJson(JsonReader reader, Type objectType, Pressure existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is long pressure)
            {
                return (Pressure)pressure;
            }

            throw new NotSupportedException($"Cannot convert from {reader.Value} to Pressure");
        }
    }
}
