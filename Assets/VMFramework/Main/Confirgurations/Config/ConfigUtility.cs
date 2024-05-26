using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Configuration
{
    public static class ConfigUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init(this IEnumerable<IConfig> configs)
        {
            foreach (var config in configs)
            {
                config.Init();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init<TConfig>(this IEnumerable<TConfig> configs) where TConfig : IConfig
        {
            foreach (var config in configs)
            {
                config.Init();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSettings(this IEnumerable<IConfig> configs)
        {
            foreach (var config in configs)
            {
                config.CheckSettings();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSettings<TConfig>(this IEnumerable<TConfig> configs) where TConfig : IConfig
        {
            foreach (var config in configs)
            {
                config.CheckSettings();
            }
        }
    }
}