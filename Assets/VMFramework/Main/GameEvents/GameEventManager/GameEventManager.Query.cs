using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.GameEvents
{
    public partial class GameEventManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameEvent GetGameEventStrictly<TGameEvent>(string id)
            where TGameEvent : IGameEvent
        {
            if (allGameEvents.TryGetValue(id, out IGameEvent gameEventInterface) == false)
            {
                throw new KeyNotFoundException($"GameEvent with id {id} not found.");
            }
            
            if (gameEventInterface is not TGameEvent typedGameEvent)
            {
                throw new InvalidCastException($"GameEvent with id {id} is not of type {typeof(TGameEvent)}.");
            }
            
            return typedGameEvent;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGameEvent GetGameEventStrictly(string id)
        {
            if (allGameEvents.TryGetValue(id, out var gameEvent) == false)
            {
                throw new KeyNotFoundException($"GameEvent with id {id} not found.");
            }
            
            return gameEvent;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGameEvent<TGameEvent>(string id, out TGameEvent gameEvent)
        {
            if (allGameEvents.TryGetValue(id, out IGameEvent gameEventInterface))
            {
                if (gameEventInterface is TGameEvent typedGameEvent)
                {
                    gameEvent = typedGameEvent;
                    return true;
                }
            }
            
            gameEvent = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGameEvent(string id, out IGameEvent gameEvent)
        {
            return allGameEvents.TryGetValue(id, out gameEvent);
        }
    }
}