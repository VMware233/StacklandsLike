#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class AudioGeneralSetting
    {
        protected override void OnHandleRegisterGameItemPrefabFromSelectedObject(
            Object obj, bool checkUnique)
        {
            if (obj is not AudioClip audioClip)
            {
                return;
            }

            if (checkUnique)
            {
                if (GamePrefabManager.GetAllGamePrefabs<AudioPreset>().Any(prefab => prefab.audioClip == audioClip))
                {
                    Debug.LogWarning($"为了放置重复，忽略了{audioClip.name}的添加");
                    return;
                }
            }

            GamePrefabWrapperCreator.CreateGamePrefabWrapper(new AudioPreset()
            {
                id = obj.name.ToSnakeCase(),
                audioClip = audioClip,
                autoCheckStop = true,
                volume = 1
            });
        }

        protected override void OnHandleUnregisterGameItemPrefabFromSelectedObject(Object obj)
        {
            if (obj is not AudioClip audioClip)
            {
                return;
            }

            GamePrefabWrapperRemover.RemoveGamePrefabWrapperWhere<AudioPreset>(prefab =>
                prefab.audioClip == audioClip);
        }

        protected override bool ShowRegisterGameItemPrefabFromSelectionButton(
            IEnumerable<Object> objects)
        {
            return objects.Any(obj => obj is AudioClip);
        }
    }
}
#endif