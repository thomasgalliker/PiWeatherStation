using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    public class EpochDateTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static DateTime Convert(long milliseconds) => Epoch.AddMilliseconds(milliseconds);
        private static long Convert(DateTime utcDateTime) => (long)(utcDateTime - Epoch).TotalMilliseconds;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Convert((DateTime)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var Value = System.Convert.ToInt64(reader.Value);
            return Convert(Value);
        }
    }
}