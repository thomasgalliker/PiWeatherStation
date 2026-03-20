using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnitsNet;

namespace WeatherDisplay.Api.Serialization
{
    internal sealed class UnitsNetIQuantityJsonConverter : JsonConverter<IQuantity>
    {
        public override IQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Deserializing UnitsNet IQuantity values is not supported.");
        }

        public override void Write(Utf8JsonWriter writer, IQuantity value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            writer.WriteString("unit", value.Unit.ToString());
            writer.WriteNumber("value", Convert.ToDouble(value.Value, CultureInfo.InvariantCulture));
            writer.WriteEndObject();
        }
    }
}
