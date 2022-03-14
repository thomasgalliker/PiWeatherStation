using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal class CelsiusTemperatureJsonConverter : JsonConverter<Temperature>
    {
        public override void WriteJson(JsonWriter writer, Temperature value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }

        public override Temperature ReadJson(JsonReader reader, Type objectType, Temperature existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is double celsius)
            {
                return Temperature.FromCelsius(celsius);
            }
            
            if (reader.Value is long celsiusLong)
            {
                return Temperature.FromCelsius(celsiusLong);
            }

            throw new NotSupportedException($"Cannot convert from {reader.Value} to °Celsius");
        }
    }
}
