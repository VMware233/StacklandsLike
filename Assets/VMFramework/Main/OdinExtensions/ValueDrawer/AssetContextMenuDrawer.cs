#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.OdinExtensions
{
    public class AssetContextMenuDrawer : OdinValueDrawer<Object>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = property.ValueEntry.WeakSmartValue;

            if (value == null)
            {
                return;
            }

            if (value is not Object obj)
            {
                return;
            }

            if (obj.IsAsset() == false)
            {
                return;
            }
            
            genericMenu.AddItem(new GUIContent("Delete Asset"), false, () =>
            {
                if (UnityEditor.EditorUtility.DisplayDialog("警告", "你确定要删除资源吗？", "确定", "取消"))
                {
                    obj.DeleteAsset();
                }
            });
        }
    }
}
#endif