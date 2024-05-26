using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Configuration
{
    public static class StructureConfigsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAddConfig<TConfig>(this IStructureConfigs<TConfig> structureConfigs,
            TConfig config) where TConfig : IConfig
        {
            if (structureConfigs.initDone)
            {
                return structureConfigs.TryAddConfigRuntime(config);
            }

            return structureConfigs.TryAddConfigEditor(config);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasConfig<TConfig>(this IStructureConfigs<TConfig> structureConfigs,
            TConfig config)
            where TConfig : IConfig
        {
            if (structureConfigs.initDone)
            {
                return structureConfigs.HasConfigRuntime(config);
            }
            
            return structureConfigs.HasConfigEditor(config);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TConfig> GetAllConfigs<TConfig>(
            this IStructureConfigs<TConfig> structureConfigs)
            where TConfig : IConfig
        {
            if (structureConfigs.initDone)
            {
                return structureConfigs.GetAllConfigsRuntime();
            }

            return structureConfigs.GetAllConfigsEditor();
        }
    }
}