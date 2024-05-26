#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using VMFramework.Core;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public class VisualElementNameAttributeDrawer : 
        GeneralValueDropdownAttributeDrawer<VisualElementNameAttribute>
    {
        protected override void Validate()
        {
            base.Validate();
            
            foreach (var parent in Property.TraverseToRoot(false, property => property.Parent))
            {
                var value = parent?.ValueEntry?.WeakSmartValue;

                if (value is IUIToolkitUIPanelPreset preset)
                {
                    return;
                }
            }

            SirenixEditorGUI.ErrorMessageBox(
                $"The property {Property.Name} is not a child of a {nameof(IUIToolkitUIPanelPreset)}.");
        }

        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            foreach (var parent in Property.TraverseToRoot(false, property => property.Parent))
            {
                var value = parent?.ValueEntry?.WeakSmartValue;

                if (value is IUIToolkitUIPanelPreset preset)
                {
                    return preset.visualTree.GetAllNamesByTypes(Attribute.VisualElementTypes)
                        .ToValueDropdownItems();
                }
            }
            
            return Enumerable.Empty<ValueDropdownItem>();
        }
    }
}
#endif