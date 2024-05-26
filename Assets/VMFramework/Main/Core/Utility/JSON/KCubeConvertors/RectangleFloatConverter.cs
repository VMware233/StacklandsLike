using System;
using Newtonsoft.Json;

namespace VMFramework.Core.JSON
{
    public sealed class RectangleFloatConverter : JsonConverter<RectangleFloat>
    {
        public override void WriteJson(JsonWriter writer, RectangleFloat value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteVector2(value.min);
            writer.WriteVector2(value.max);
            writer.WriteEndArray();
        }

        public override RectangleFloat ReadJson(JsonReader reader, Type objectType, RectangleFloat existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new JsonSerializationException("Error deserializing RectangleFloat. Expected StartArray token.");
            }

            reader.Read();
            
            var min = reader.ReadVector2();
            reader.Read();
            var max = reader.ReadVector2();
            reader.Read();
            
            if (reader.TokenType != JsonToken.EndArray)
            {
                throw new JsonSerializationException("Error deserializing RectangleFloat. Expected EndArray token.");
            }
            
            return new(min, max);
        }
    }
}