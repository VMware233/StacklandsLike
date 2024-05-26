#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.OdinExtensions
{
    internal sealed class EnumChooserConfigContextMenuDrawer<T, TEnum> : OdinValueDrawer<T>, IDefinesGenericMenuItems
        where T : ICollectionChooserConfig<TEnum>
        where TEnum : struct, Enum
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var chooser = ValueEntry.SmartValue;

            if (chooser == null)
            {
                return;
            }
            
            genericMenu.AddItem(new GUIContent("添加所有枚举值"), false, AddAllEnumValues);

            void AddAllEnumValues()
            {
                foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
                {
                    if (chooser.ContainsValue((TEnum)enumValue))
                    {
                        continue;
                    }
                    
                    chooser.AddValue((TEnum)enumValue);
                }
            }
        }
    }
}
#endif