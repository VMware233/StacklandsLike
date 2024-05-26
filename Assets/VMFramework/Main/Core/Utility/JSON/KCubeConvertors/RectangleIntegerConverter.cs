using System;
using Newtonsoft.Json;

namespace VMFramework.Core.JSON
{
    public sealed class RectangleIntegerConverter : JsonConverter<RectangleInteger>
    {
        public override void WriteJson(JsonWriter writer, RectangleInteger value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteVector2Int(value.min);
            writer.WriteVector2Int(value.max);
            writer.WriteEndArray();
        }

        public override RectangleInteger ReadJson(JsonReader reader, Type objectType,
            RectangleInteger existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonSerializationException("Error deserializing RectangleInteger. Expected StartArray token.");
            }

            reader.Read();
            
            var min = reader.ReadVector2Int();
            reader.Read();
            var max = reader.ReadVector2Int();
            reader.Read();
            
            if (reader.TokenType != JsonToken.EndArray)
            {
                throw new JsonSerializationException("Error deserializing RectangleInteger. Expected EndArray token.");
            }
            
            return new(min, max);
        }
    }
}