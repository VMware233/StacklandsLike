using System.Collections.Generic;

namespace VMFramework.Configuration
{
    public partial interface IStructureConfigs<TConfig> : IConfig where TConfig : IConfig
    {
        public bool TryAddConfigEditor(TConfig config);

        public bool TryAddConfigRuntime(TConfig config);
        
        public bool HasConfigEditor(TConfig config);
        
        public bool HasConfigRuntime(TConfig config);

        public IEnumerable<TConfig> GetAllConfigsEditor();
        
        public IEnumerable<TConfig> GetAllConfigsRuntime();
    }
}