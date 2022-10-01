using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.Wiewarm.Converters
{
    internal class TemperatureJsonConverter : JsonConverter<Temperature>
    {
        public override void WriteJson(JsonWriter writer, Temperature value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Value}");
        }

        public override Temperature ReadJson(JsonReader reader, Type objectType, Temperature existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is string stringValue && double.TryParse(stringValue, out var celsius))
            {
                return Temperature.FromCelsius(celsius);
            }

            return default(Temperature);
        }
    }
}
