using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherDisplay.Model.Wiewarm.Converters
{
    public abstract class AbstractJTokenToListJsonConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObject = JObject.Load(reader);

                var elements = jObject.Children()
                    .Select(c => c.First().ToObject<T>())
                    .ToList();

                return elements;
            }

            throw new NotSupportedException();
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}