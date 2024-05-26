using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Core.JSON
{
    public sealed class Vector2IntConverter : JsonConverter<Vector2Int>
    {
        public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
        {
            writer.WriteVector2Int(value);
        }

        public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.ReadVector2Int();
        }
    }
}