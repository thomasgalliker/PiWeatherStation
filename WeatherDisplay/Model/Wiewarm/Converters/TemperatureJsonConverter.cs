using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace WeatherDisplay.Model.Wiewarm.Converters
{
    internal class TemperatureJsonConverter : JsonConverter<Temperature>
    {
        public override Temperature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (double.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var celsius))
                {
                    return Temperature.FromDegreesCelsius(celsius);
                }
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out var numberValue))
            {
                return Temperature.FromDegreesCelsius(numberValue);
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, Temperature value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
