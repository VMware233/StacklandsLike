#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public partial class DefaultContainerItemConsumptionConfig<TItem, TItemPrefab>
    {
        protected virtual IEnumerable<ValueDropdownItem> GetItemPrefabNameList()
        {
            return GamePrefabManager.GetGamePrefabNameListByType(typeof(TItemPrefab));
        }
    }
}
#endif