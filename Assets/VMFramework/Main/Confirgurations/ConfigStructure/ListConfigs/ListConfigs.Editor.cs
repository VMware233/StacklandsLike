#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core.Linq;

namespace VMFramework.Configuration
{
    public partial class ListConfigs<TConfig>
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            configs ??= new();

            OnConfigsCollectionChanged();
        }

        private void OnConfigsCollectionChanged()
        {
            foreach (var (index, config) in configs.Enumerate())
            {
                config.index = index;
                config.listConfigs = this;
            }
        }

        public IEnumerable<ValueDropdownItem<int>> GetNameList()
        {
            return configs.Select((config, index) =>
                new ValueDropdownItem<int>(config.name, index));
        }
    }
}
#endif