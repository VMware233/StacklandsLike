using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    public sealed partial class DictionaryConfigs<TID, TConfig> : StructureConfigs<TConfig>,
        IDictionaryConfigs<TID, TConfig>
        where TConfig : IConfig, IIDOwner<TID>
    {
        [ShowInInspector]
        [HideInEditorMode]
        private Dictionary<TID, TConfig> configsRuntime = new();

        #region CheckSettings

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            configsRuntime = new();
        }

        #endregion

        #region Add Config

        public override bool TryAddConfigRuntime(TConfig config)
        {
            return configsRuntime.TryAdd(config.id, config);
        }

        #endregion

        #region Remove Config

        public bool RemoveConfigEditor(TID id)
        {
            foreach (var config in configs)
            {
                if (config.id.Equals(id))
                {
                    configs.Remove(config);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveConfigRuntime(TID id)
        {
            return configsRuntime.Remove(id);
        }

        #endregion

        #region Get Config

        public override IEnumerable<TConfig> GetAllConfigsRuntime()
        {
            return configsRuntime.Values;
        }

        public TConfig GetConfigEditor(TID id)
        {
            foreach (var config in configs)
            {
                if (config.id.Equals(id))
                {
                    return config;
                }
            }

            return default;
        }

        public TConfig GetConfigRuntime(TID id)
        {
            return configsRuntime.GetValueOrDefault(id);
        }

        #endregion

        #region Has Config

        public override bool HasConfigEditor(TConfig config)
        {
            if (config == null)
            {
                return false;
            }
            
            return GetConfigEditor(config.id) != null;
        }

        public override bool HasConfigRuntime(TConfig config)
        {
            if (config == null)
            {
                return false;
            }
            
            return GetConfigRuntime(config.id)!= null;
        }

        #endregion
    }
}
