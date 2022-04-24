using System;
using System.Globalization;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal class RegionInfoJsonConverter : JsonConverter<RegionInfo>
    {
        public override void WriteJson(JsonWriter writer, RegionInfo value, JsonSerializer serializer)
        {
            writer.WriteValue(value.TwoLetterISORegionName);
        }

        public override RegionInfo ReadJson(JsonReader reader, Type objectType, RegionInfo existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is string twoLetterISORegionName)
            {
                return new RegionInfo(twoLetterISORegionName);
            }

            throw new NotSupportedException($"Cannot convert from {reader.Value} to RegionInfo");
        }
    }
}
