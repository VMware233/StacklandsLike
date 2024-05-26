using System;
using System.Runtime.CompilerServices;

namespace VMFramework.GameEvents
{
    public partial class GameEventManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCallback(string id, Delegate callback, int priority = GameEventPriority.TINY)
        {
            var gameEvent = GetGameEventStrictly(id);
            
            gameEvent.AddCallback(callback, priority);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCallback(string id, Action<BoolInputGameEvent> callback,
            int priority = GameEventPriority.TINY)
        {
            var gameEvent = GetGameEventStrictly<BoolInputGameEvent>(id);

            gameEvent.AddCallback(callback, priority);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCallback(string id, Action<FloatInputGameEvent> callback,
            int priority = GameEventPriority.TINY)
        {
            var gameEvent = GetGameEventStrictly<FloatInputGameEvent>(id);

            gameEvent.AddCallback(callback, priority);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCallback(string id, Action<Vector2InputGameEvent> callback,
            int priority = GameEventPriority.TINY)
        {
            var gameEvent = GetGameEventStrictly<Vector2InputGameEvent>(id);

            gameEvent.AddCallback(callback, priority);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCallback<TGameEvent>(string id, Action<TGameEvent> callback,
            int priority = GameEventPriority.TINY)
            where TGameEvent : GameEvent<TGameEvent>
        {
            var gameEvent = GetGameEventStrictly<TGameEvent>(id);

            gameEvent.AddCallback(callback, priority);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCallback(string id, Delegate callback)
        {
            var gameEvent = GetGameEventStrictly(id);
            
            gameEvent.RemoveCallback(callback);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCallback(string id, Action<BoolInputGameEvent> callback)
        {
            var gameEvent = GetGameEventStrictly<BoolInputGameEvent>(id);
            
            gameEvent.RemoveCallback(callback);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCallback(string id, Action<FloatInputGameEvent> callback)
        {
            var gameEvent = GetGameEventStrictly<FloatInputGameEvent>(id);
            
            gameEvent.RemoveCallback(callback);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCallback(string id, Action<Vector2InputGameEvent> callback)
        {
            var gameEvent = GetGameEventStrictly<Vector2InputGameEvent>(id);
            
            gameEvent.RemoveCallback(callback);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCallback<TGameEvent>(string id, Action<TGameEvent> callback)
            where TGameEvent : GameEvent<TGameEvent>
        {
            var gameEvent = GetGameEventStrictly<TGameEvent>(id);
            
            gameEvent.RemoveCallback(callback);
        }
    }
}