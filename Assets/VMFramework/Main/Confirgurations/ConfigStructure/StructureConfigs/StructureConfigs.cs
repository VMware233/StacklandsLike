using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public abstract partial class StructureConfigs<TConfig> : BaseConfig, IStructureConfigs<TConfig>
        where TConfig : IConfig
    {
        [LabelText("设置")]
        [ListDrawerSettings(ShowFoldout = false)]
        [SerializeField]
        protected List<TConfig> configs = new();
        
        public override void CheckSettings()
        {
            base.CheckSettings();

            configs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            configs.Init();

            foreach (var config in configs)
            {
                if (TryAddConfigRuntime(config) == false)
                {
                    Debug.LogWarning($"Could not add config {config}");
                }
            }
        }
        
        public abstract bool HasConfigEditor(TConfig config);
        
        public abstract bool HasConfigRuntime(TConfig config);

        public IEnumerable<TConfig> GetAllConfigsEditor()
        {
            return configs;
        }
        
        public abstract IEnumerable<TConfig> GetAllConfigsRuntime();

        public bool TryAddConfigEditor(TConfig config)
        {
            if (HasConfigEditor(config) == false)
            {
                configs.Add(config);
                return true;
            }

            return false;
        }
        
        public abstract bool TryAddConfigRuntime(TConfig config);
        
        public override string ToString()
        {
            return configs.Select<TConfig, INameOwner>().Select(nameOwner => nameOwner.name).Join(",");
        }
    }
}