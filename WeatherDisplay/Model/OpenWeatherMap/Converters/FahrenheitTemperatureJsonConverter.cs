using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal class FahrenheitTemperatureJsonConverter : JsonConverter<Temperature>
    {
        public override void WriteJson(JsonWriter writer, Temperature value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }

        public override Temperature ReadJson(JsonReader reader, Type objectType, Temperature existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is double fahrenheit)
            {
                return Temperature.FromFahrenheit(fahrenheit);
            }

            if (reader.Value is long fahrenheitLong)
            {
                return Temperature.FromFahrenheit(fahrenheitLong);
            }

            throw new NotSupportedException($"Cannot convert from {reader.Value} to Fahrenheit");
        }
    }
}
