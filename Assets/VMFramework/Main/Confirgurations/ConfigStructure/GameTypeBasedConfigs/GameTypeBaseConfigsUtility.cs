using System.Runtime.CompilerServices;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public static class GameTypeBaseConfigsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TConfig GetConfigRuntime<TConfig>(
            this IGameTypeBasedConfigs<TConfig> gameTypeBasedConfigs, IReadOnlyGameTypeSet gameTypeSet)
            where TConfig : IConfig
        {
            foreach (var leafGameTypeID in gameTypeSet.leafGameTypesID)
            {
                if (gameTypeBasedConfigs.TryGetConfigRuntime(leafGameTypeID, out var config))
                {
                    return config;
                }
            }

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetConfigRuntime<TConfig>(
            this IGameTypeBasedConfigs<TConfig> gameTypeBasedConfigs, IReadOnlyGameTypeSet gameTypeSet, out TConfig config)
            where TConfig : IConfig
        {
            foreach (var leafGameTypeID in gameTypeSet.leafGameTypesID)
            {
                if (gameTypeBasedConfigs.TryGetConfigRuntime(leafGameTypeID, out config))
                {
                    return true;
                }
            }
            
            config = default;
            return false;
        }
    }
}