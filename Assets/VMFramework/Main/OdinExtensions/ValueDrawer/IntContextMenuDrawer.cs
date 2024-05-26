#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    internal sealed class IntContextMenuDrawer : OdinValueDrawer<int>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            genericMenu.AddSeparator("");
            
            genericMenu.AddItem(new GUIContent($"Set to {int.MaxValue}"), false, () =>
            {
                ValueEntry.SmartValue = int.MaxValue;
            });
            
            genericMenu.AddItem(new GUIContent($"Set to {int.MinValue}"), false, () =>
            {
                ValueEntry.SmartValue = int.MinValue;
            });
        }
    }
}

#endif