#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    [InitializeOnLoad]
    public class ColorfulHierarchy
    {
        static ColorfulHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
        }

        private static void OnHierarchyWindow(int instanceID, Rect selectionRect)
        {
            var generalSetting = GameCoreSetting.colorfulHierarchyGeneralSetting;

            if (generalSetting == null)
            {
                return;
            }

            Object instance = EditorUtility.InstanceIDToObject(instanceID);

            if (instance != null)
            {
                foreach (var preset in generalSetting.colorPresets)
                {
                    if (preset.keyChar.IsNullOrEmpty())
                    {
                        continue;
                    }

                    if (instance.name.TrimStart().StartsWith(preset.keyChar))
                    {
                        string newName = instance.name[preset.keyChar.Length..];

                        EditorGUI.DrawRect(selectionRect, preset.backgroundColor);

                        GUIStyle newStyle = new GUIStyle
                        {
                            alignment = preset.textAlignment,
                            fontStyle = preset.fontStyle,
                            normal = new GUIStyleState()
                            {
                                textColor = preset.textColor,
                            }
                        };

                        if (preset.autoUpperLetters)
                        {
                            newName = newName.ToUpper();
                        }

                        EditorGUI.LabelField(selectionRect, newName, newStyle);
                    }
                }
            }
        }
    }
}

#endif