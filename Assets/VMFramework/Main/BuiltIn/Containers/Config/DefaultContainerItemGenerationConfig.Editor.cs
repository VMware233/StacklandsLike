#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public partial class DefaultContainerItemGenerationConfig<TItem, TItemPrefab>
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            count ??= new SingleVectorChooserConfig<int>(1);
        }
        
        protected virtual IEnumerable<ValueDropdownItem> GetItemPrefabNameList()
        {
            return GamePrefabManager.GetGamePrefabNameListByType(typeof(TItemPrefab));
        }
    }
}
#endif