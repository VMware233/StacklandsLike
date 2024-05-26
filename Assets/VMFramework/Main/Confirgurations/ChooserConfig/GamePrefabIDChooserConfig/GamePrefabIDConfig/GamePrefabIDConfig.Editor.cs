#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    public partial struct GamePrefabIDConfig<TGamePrefab>
    {
        private IEnumerable<ValueDropdownItem> GetNameList()
        {
            return GamePrefabManager.GetGamePrefabNameListByType(typeof(TGamePrefab));
        }
    }
}
#endif