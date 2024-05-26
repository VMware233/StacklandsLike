using System;
using Newtonsoft.Json;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Core.JSON
{
    public sealed class GameTypeConverter : JsonConverter<GameType>
    {
        public override void WriteJson(JsonWriter writer, GameType value, JsonSerializer serializer)
        {
            writer.WriteValue(value.id);
        }

        public override GameType ReadJson(JsonReader reader, Type objectType, GameType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return (string)reader.Value;
        }
    }
}