using System.Runtime.CompilerServices;

namespace VMFramework.GameEvents
{
    public partial class GameEventManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Propagate(string id)
        {
            var gameEvent = GetGameEventStrictly(id);
            
            gameEvent.Propagate();
        }
    }
}