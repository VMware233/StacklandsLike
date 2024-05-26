#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.AttributePriority)]
    public class LayerAttributeDrawer : OdinAttributeDrawer<LayerAttribute, int>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueEntry.SmartValue = label == null
                ? EditorGUILayout.LayerField(ValueEntry.SmartValue)
                : EditorGUILayout.LayerField(label, ValueEntry.SmartValue);
        }
    }
}
#endif