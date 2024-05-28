#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using VMFramework.Core;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public sealed class UGUINameAttributeDrawer : GeneralValueDropdownAttributeDrawer<UGUINameAttribute>
    {
        protected override void Validate()
        {
            base.Validate();
            
            foreach (var parent in Property.TraverseToRoot(false, property => property.Parent))
            {
                var value = parent?.ValueEntry?.WeakSmartValue;

                if (value is IUGUIPanelPreset preset)
                {
                    return;
                }
            }

            SirenixEditorGUI.ErrorMessageBox(
                $"The property {Property.Name} is not a child of a {nameof(IUGUIPanelPreset)}.");
        }

        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            foreach (var parent in Property.TraverseToRoot(false, property => property.Parent))
            {
                var value = parent?.ValueEntry?.WeakSmartValue;

                if (value is IUGUIPanelPreset preset)
                {
                    return preset.prefab.transform.GetAllChildrenNames(Attribute.UGUITypes, true)
                        .ToValueDropdownItems();
                }
            }
            
            return Enumerable.Empty<ValueDropdownItem>();
        }
    }
}
#endif