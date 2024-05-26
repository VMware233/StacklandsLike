using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Core.JSON
{
    public sealed class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteVector2(value);
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.ReadVector2();
        }
    }
}