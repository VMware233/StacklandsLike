using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Core.JSON
{
    public sealed class ColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteColor(value);
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.ReadColor();
        }
    }
}