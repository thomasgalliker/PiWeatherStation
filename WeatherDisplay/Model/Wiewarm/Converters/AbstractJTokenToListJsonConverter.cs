using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherDisplay.Model.Wiewarm.Converters
{
    public abstract class AbstractJTokenToListJsonConverter<T> : JsonConverter<IReadOnlyCollection<T>>
    {
        public override IReadOnlyCollection<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return [];
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected {JsonTokenType.StartObject} but got {reader.TokenType}.");
            }

            using var document = JsonDocument.ParseValue(ref reader);
            var elements = new List<T>();

            foreach (var property in document.RootElement.EnumerateObject())
            {
                var element = property.Value.Deserialize<T>(options);
                if (element is not null)
                {
                    elements.Add(element);
                }
            }

            return elements;
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyCollection<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
