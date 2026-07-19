using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherDisplay.Model.Wiewarm.Converters
{
    internal class WiewarmDateTimeJsonConverter : JsonConverter<DateTime>
    {
        private static readonly string[] DateFormats =
        [
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss.FFF",
            "yyyy-MM-dd HH:mm:ss.FFFF",
            "yyyy-MM-dd HH:mm:ss.FFFFF",
            "yyyy-MM-dd HH:mm:ss.FFFFFF",
            "yyyy-MM-dd HH:mm:ss.FFFFFFF",
        ];

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (DateTime.TryParseExact(stringValue, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var dateTime))
                {
                    return dateTime;
                }
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormats[0], CultureInfo.InvariantCulture));
        }
    }
}
