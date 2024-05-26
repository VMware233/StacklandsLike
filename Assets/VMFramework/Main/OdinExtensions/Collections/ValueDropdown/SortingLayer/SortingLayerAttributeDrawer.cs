#if UNITY_EDITOR
using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.AttributePriority)]
    public class SortingLayerAttributeDrawer : OdinAttributeDrawer<SortingLayerAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            var layerNames =
                SortingLayer.layers.Select(layer => layer.name).ToArray();

            var currentLayerName = ValueEntry.SmartValue;
            var selectedLayerIndex = Array.IndexOf(layerNames, currentLayerName);
            EditorGUI.BeginChangeCheck();

            var newSelectedIndex = EditorGUI.Popup(rect, label.text,
                selectedLayerIndex, layerNames);
            if (EditorGUI.EndChangeCheck())
            {
                ValueEntry.SmartValue = layerNames[newSelectedIndex];
            }
        }
    }
}
#endif