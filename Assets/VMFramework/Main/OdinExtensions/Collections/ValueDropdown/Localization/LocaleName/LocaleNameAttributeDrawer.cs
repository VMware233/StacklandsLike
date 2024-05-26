#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.OdinExtensions
{
    public class LocaleNameAttributeDrawer : GeneralValueDropdownAttributeDrawer<LocaleNameAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return LocalizationEditorManager.GetLocaleNameList();
        }
    }
}
#endif