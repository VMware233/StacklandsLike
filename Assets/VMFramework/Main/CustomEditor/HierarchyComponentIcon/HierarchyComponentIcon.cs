#if UNITY_EDITOR
using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;

    public class HierarchyComponentIcon
    {
        #region --- VAR ---

        private const int DEFAULT_MAX_ICON_NUM = 5;

        private const int DEFAULT_ICON_SIZE = 16;

        private static int maxIconNum
        {
            get
            {
                var setting = GameCoreSetting
                    .hierarchyComponentIconGeneralSetting;

                if (setting != null)
                {
                    return setting.maxIconNum;
                }

                return DEFAULT_MAX_ICON_NUM;
            }
        }

        private static int iconSize
        {
            get
            {
                var setting = GameCoreSetting
                    .hierarchyComponentIconGeneralSetting;

                if (setting != null)
                {
                    return setting.iconSize;
                }

                return DEFAULT_ICON_SIZE;

            }
        }

        private static HashSet<Type> hideTypes =
            new()
            {
                typeof(Transform),
                typeof(ParticleSystemRenderer),
                typeof(CanvasRenderer),
            };

        private static Transform OffsetObject = null;
        private static int Offset = 0;

        #endregion

        #region --- MSG ---

        [InitializeOnLoadMethod]
        public static void Init()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyComponentIconGUI;
        }

        public static void HierarchyComponentIconGUI(int instanceID, Rect rect)
        {
            // Check
            Object tempObj = EditorUtility.InstanceIDToObject(instanceID);
            if (!tempObj)
            {
                return;
            }

            // fix rect
            rect.width += rect.x;
            rect.x = 0;

            // Logic
            GameObject obj = tempObj as GameObject;
            var components = new List<Component>();
            foreach (var component in obj.GetComponents<Component>())
            {
                if (component == null)
                {
                    continue;
                }

                if (IsTypeIconRequiredToHide(component.GetType()))
                {
                    continue;
                }

                components.Add(component);
            }

            int iconSize = HierarchyComponentIcon.iconSize;
            int y = 1;
            int offset = obj.transform == OffsetObject ? Offset : 0;

            // Main
            for (int i = 0; i + offset < components.Count && i < maxIconNum; i++)
            {
                Component com = components[i + offset];

                Texture2D texture = AssetPreview.GetMiniThumbnail(com);

                if (texture)
                {
                    GUI.DrawTexture(
                        new Rect(rect.width - (iconSize + 1) * (i + 1), rect.y + y,
                            iconSize, iconSize), texture);
                }
            }

            // More Button
            if (components.Count == maxIconNum + 1)
            {
                Texture2D texture = AssetPreview.GetMiniThumbnail(components[^1]);
                if (texture)
                {
                    GUI.DrawTexture(
                        new Rect(
                            rect.width - (iconSize + 1) * (components.Count - 1 + 1),
                            rect.y + y, iconSize, iconSize),
                        texture);
                }
            }
            else if (components.Count > maxIconNum)
            {
                GUIStyle style = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 9,
                    alignment = TextAnchor.MiddleCenter
                };

                if (GUI.Button(
                        new Rect(rect.width - (iconSize + 2) * (maxIconNum + 1),
                            rect.y + y, 22, iconSize), "•••",
                        style))
                {
                    if (OffsetObject != obj.transform)
                    {
                        OffsetObject = obj.transform;
                        Offset = 0;
                    }

                    Offset += maxIconNum;
                    if (Offset >= components.Count)
                    {
                        Offset = 0;
                    }
                }
            }
        }

        #endregion

        #region --- LGC ---

        private static bool IsTypeIconRequiredToHide(Type type)
        {
            foreach (var hideType in hideTypes)
            {
                if (type == hideType || type.IsSubclassOf(hideType))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
#endif