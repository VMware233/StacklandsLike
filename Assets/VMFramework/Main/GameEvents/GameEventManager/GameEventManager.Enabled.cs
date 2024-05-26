using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.GameEvents
{
    public partial class GameEventManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Enable(string id)
        {
            var gameEvent = GetGameEventStrictly(id);
            
            gameEvent.Enable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Disable(string id)
        {
            var gameEvent = GetGameEventStrictly(id);
            
            gameEvent.Disable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Enable(IEnumerable<string> ids)
        {
            if (ids == null)
            {
                return;
            }
            
            foreach (var id in ids)
            {
                Enable(id);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Disable(IEnumerable<string> ids)
        {
            if (ids == null)
            {
                return;
            }
            
            foreach (var id in ids)
            {
                Disable(id);
            }
        }
    }
}