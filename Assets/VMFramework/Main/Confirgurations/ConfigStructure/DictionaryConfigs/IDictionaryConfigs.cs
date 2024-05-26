using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IDictionaryConfigs<in TID, TConfig> : IStructureConfigs<TConfig>
        where TConfig : IConfig
    {
        public TConfig GetConfigEditor(TID id);
        
        public TConfig GetConfigRuntime(TID id);

        public bool RemoveConfigEditor(TID id);
        
        public bool RemoveConfigRuntime(TID id);
    }
}