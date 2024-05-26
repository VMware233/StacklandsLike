using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VMFramework.Core.JSON
{
    public static class JSONConverters
    {
        public static JsonConverter[] converters = {
            new Vector2Converter(),
            new Vector2IntConverter(),
            new Vector3Converter(),
            new Vector3IntConverter(),
            new Vector4Converter(),
            new ColorConverter(),
            new RectangleIntegerConverter(),
            new RectangleFloatConverter(),
            new GameTypeConverter(),
            new StringEnumConverter(),
        };
    }
}