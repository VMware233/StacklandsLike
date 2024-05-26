using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Core.JSON
{
    public sealed class Vector3IntConverter : JsonConverter<Vector3Int>
    {
        public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
        {
            writer.WriteValue($"({value.x}, {value.y}, {value.z})");
        }

        public override Vector3Int ReadJson(JsonReader reader, Type objectType, Vector3Int existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            if (value == null)
            {
                return existingValue;
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length != 3 || int.TryParse(split[0], out int x) == false ||
                int.TryParse(split[1], out int y) == false || int.TryParse(split[2], out int z) == false)
            {
                JSONDebugUtility.IsInvalidValue<Vector3Int>(value);
                return existingValue;
            }

            return new Vector3Int(x, y, z);
        }
    }
}