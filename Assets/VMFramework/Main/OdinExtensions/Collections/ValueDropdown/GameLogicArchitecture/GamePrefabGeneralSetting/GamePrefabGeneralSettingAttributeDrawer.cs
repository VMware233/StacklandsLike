#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GamePrefabGeneralSettingAttributeDrawer : 
        GeneralValueDropdownAttributeDrawer<GamePrefabGeneralSettingAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return GamePrefabGeneralSettingUtility.GetGamePrefabGeneralSettingNameList();
        }
    }
}
#endif