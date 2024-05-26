using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Core.JSON
{
    public sealed class Vector4Converter : JsonConverter<Vector4>
    {
        public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
        {
            writer.WriteVector4(value);
        }

        public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.ReadVector4();
        }
    }
}