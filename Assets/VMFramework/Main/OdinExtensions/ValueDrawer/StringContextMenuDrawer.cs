#if UNITY_EDITOR && ODIN_INSPECTOR
using VMFramework.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    internal sealed class StringContextMenuDrawer : OdinValueDrawer<string>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property,
            GenericMenu genericMenu)
        {
            if (property.ValueEntry.WeakSmartValue is not string str)
            {
                return;
            }

            if (property.GetAttribute<ValueDropdownAttribute>() != null)
            {
                return;
            }

            if (str.IsEmptyAfterTrim())
            {
                return;
            }

            genericMenu.AddSeparator("");

            genericMenu.AddItem(new GUIContent("Pascal Case"), false, () =>
            {
                property.ValueEntry.WeakSmartValue = str.ToPascalCase(" ");
            });

            genericMenu.AddItem(new GUIContent("Snake Case"), false, () =>
            {
                property.ValueEntry.WeakSmartValue = str.ToSnakeCase();
            });

            if (str.Contains(' '))
            {
                genericMenu.AddItem(new GUIContent("Remove Spaces"), false, () =>
                {
                    property.ValueEntry.WeakSmartValue = str.Replace(" ", "");
                });
            }
        }
    }
}

#endif