using System.Runtime.CompilerServices;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGamePrefabName(string id)
        {
            var gamePrefab = GetGamePrefab(id);
            
            return gamePrefab?.name;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGamePrefabNameWithWarning(string id)
        {
            if (TryGetGamePrefabWithWarning(id, out var gamePrefab))
            {
                return gamePrefab.name;
            }
            
            return null;
        }
    }
}