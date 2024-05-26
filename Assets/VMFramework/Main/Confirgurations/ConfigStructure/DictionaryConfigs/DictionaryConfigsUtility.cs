using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public static class DictionaryConfigsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TConfig GetConfig<TID, TConfig>(this IDictionaryConfigs<TID, TConfig> dictionaryConfigs,
            TID id)
            where TConfig : IConfig
        {
            if (dictionaryConfigs.initDone)
            {
                return dictionaryConfigs.GetConfigRuntime(id);
            }
            
            return dictionaryConfigs.GetConfigEditor(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetConfigEditor<TID, TConfig>(this IDictionaryConfigs<TID, TConfig> dictionaryConfigs,
            TID id, out TConfig config)
            where TConfig : IConfig
        {
            config = dictionaryConfigs.GetConfigEditor(id);
            return config!= null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetConfigRuntime<TID, TConfig>(
            this IDictionaryConfigs<TID, TConfig> dictionaryConfigs, TID id, out TConfig config)
            where TConfig : IConfig
        {
            config = dictionaryConfigs.GetConfigRuntime(id);
            return config != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetConfig<TID, TConfig>(this IDictionaryConfigs<TID, TConfig> dictionaryConfigs,
            TID id, out TConfig config)
            where TConfig : IConfig
        {
            if (dictionaryConfigs.initDone)
            {
                config = dictionaryConfigs.GetConfigRuntime(id);
                return config!= null;
            }
            
            config = dictionaryConfigs.GetConfigEditor(id);
            return config!= null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasConfig<TID, TConfig>(this IDictionaryConfigs<TID, TConfig> dictionaryConfigs,
            TID id)
            where TConfig : IConfig
        {
            if (dictionaryConfigs.initDone)
            {
                return dictionaryConfigs.GetConfigRuntime(id) != null;
            }
            
            return dictionaryConfigs.GetConfigEditor(id) != null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveConfig<TID, TConfig>(this IDictionaryConfigs<TID, TConfig> dictionaryConfigs,
            TID id)
            where TConfig : IConfig
        {
            if (dictionaryConfigs.initDone)
            {
                return dictionaryConfigs.RemoveConfigRuntime(id);
            }
            
            return dictionaryConfigs.RemoveConfigEditor(id);
        }
    }
}