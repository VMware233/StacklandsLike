using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Core.JSON
{
    public static class VectorJSONSerializeUtility
    {
        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVector2Int(this JsonWriter writer, Vector2Int vector)
        {
            writer.WriteValue($"({vector.x}, {vector.y})");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ReadVector2Int(this JsonReader reader)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector2Int. Value: {reader.Value} must be a string.");
            }
            
            var value = (string) reader.Value;
            
            if (value == null)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector2Int. Value cannot be null.");
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length != 2 || int.TryParse(split[0], out var x) == false ||
                int.TryParse(split[1], out var y) == false)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector2Int. Value: {reader.Value} is not a valid Vector2Int.");
            }

            return new Vector2Int(x, y);
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVector3Int(this JsonWriter writer, Vector3Int vector)
        {
            writer.WriteValue($"({vector.x}, {vector.y}, {vector.z})");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReadVector3Int(this JsonReader reader)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector3Int. Value: {reader.Value} must be a string.");
            }
            
            var value = (string) reader.Value;
            
            if (value == null)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector3Int. Value cannot be null.");
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length!= 3 || int.TryParse(split[0], out var x) == false ||
                int.TryParse(split[1], out var y) == false || int.TryParse(split[2], out var z) == false)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector3Int. Value: {reader.Value} is not a valid Vector3Int.");
            }

            return new Vector3Int(x, y, z);
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVector2(this JsonWriter writer, Vector2 vector)
        {
            writer.WriteValue($"({vector.x}, {vector.y})");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ReadVector2(this JsonReader reader)
        {
            if (reader.TokenType!= JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector2. Value: {reader.Value} must be a string.");
            }
            
            var value = (string) reader.Value;
            
            if (value == null)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector2. Value cannot be null.");
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length!= 2 || float.TryParse(split[0], out var x) == false ||
                float.TryParse(split[1], out var y) == false)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector2. Value: {reader.Value} is not a valid Vector2.");
            }

            return new Vector2(x, y);
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVector3(this JsonWriter writer, Vector3 vector)
        {
            writer.WriteValue($"({vector.x}, {vector.y}, {vector.z})");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReadVector3(this JsonReader reader)
        {
            if (reader.TokenType!= JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector3. Value: {reader.Value} must be a string.");
            }
            
            var value = (string) reader.Value;
            
            if (value == null)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector3. Value cannot be null.");
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length!= 3 || float.TryParse(split[0], out var x) == false ||
                float.TryParse(split[1], out var y) == false || float.TryParse(split[2], out var z) == false)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector3. Value: {reader.Value} is not a valid Vector3.");
            }

            return new Vector3(x, y, z);
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVector4(this JsonWriter writer, Vector4 vector)
        {
            writer.WriteValue($"({vector.x}, {vector.y}, {vector.z}, {vector.w})");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReadVector4(this JsonReader reader)
        {
            if (reader.TokenType!= JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector4. Value: {reader.Value} must be a string.");
            }
            
            var value = (string) reader.Value;
            
            if (value == null)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector4. Value cannot be null.");
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length!= 4 || float.TryParse(split[0], out var x) == false ||
                float.TryParse(split[1], out var y) == false || float.TryParse(split[2], out var z) == false ||
                float.TryParse(split[3], out var w) == false)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Vector4. Value: {reader.Value} is not a valid Vector4.");
            }

            return new Vector4(x, y, z, w);
        }

        #endregion

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteColor(this JsonWriter writer, Color color)
        {
            writer.WriteValue($"({color.r}, {color.g}, {color.b}, {color.a})");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReadColor(this JsonReader reader)
        {
            if (reader.TokenType!= JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Color. Value: {reader.Value} must be a string.");
            }
            
            var value = (string) reader.Value;
            
            if (value == null)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Color. Value cannot be null.");
            }

            value = value.Trim('(', ')');
            var split = value.Split(',');

            if (split.Length!= 4 || float.TryParse(split[0], out var r) == false ||
                float.TryParse(split[1], out var g) == false || float.TryParse(split[2], out var b) == false ||
                float.TryParse(split[3], out var a) == false)
            {
                throw new JsonSerializationException(
                    $"Error deserializing Color. Value: {reader.Value} is not a valid Color.");
            }

            return new Color(r, g, b, a);
        }

        #endregion
    }
}