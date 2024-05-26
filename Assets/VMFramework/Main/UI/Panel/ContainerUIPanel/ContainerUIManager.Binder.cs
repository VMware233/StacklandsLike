using System.Collections.Generic;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public partial class ContainerUIManager
    {
        private static readonly Dictionary<string, string> containerBinder = new();

        public static void BindContainerUITo(string containerUIPanelID, string containerID)
        {
            if (GamePrefabManager.TryGetGamePrefab<UIPanelPreset>(containerUIPanelID, out var panelPreset) ==
                false)
            {
                Debug.LogWarning($"未找到ID为{containerUIPanelID}的{nameof(UIPanelPreset)}");
                return;
            }

            if (containerBinder.ContainsKey(containerID))
            {
                Debug.LogWarning($"已经存在ID为容器UI:{containerUIPanelID}" + $"到容器:{containerID}的绑定，将覆盖旧的绑定");
            }

            containerBinder[containerID] = containerUIPanelID;
        }

        public static bool TryGetContainerUI(string containerID, out string containerUIPanelID)
        {
            return containerBinder.TryGetValue(containerID, out containerUIPanelID);
        }
    }
}