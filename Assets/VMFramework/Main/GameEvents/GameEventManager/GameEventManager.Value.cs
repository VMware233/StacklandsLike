using UnityEngine;

namespace VMFramework.GameEvents
{
    public partial class GameEventManager
    {
        public static bool GetBool(string id)
        {
            var gameEvent = GetGameEventStrictly<IBoolInputGameEvent>(id);

            return gameEvent.value;
        }
        
        public static float GetFloat(string id)
        {
            var gameEvent = GetGameEventStrictly<IFloatInputGameEvent>(id);

            return gameEvent.value;
        }

        public static Vector2 GetVector2(string id)
        {
            var gameEvent = GetGameEventStrictly<IVector2InputGameEvent>(id);

            return gameEvent.value;
        }
    }
}