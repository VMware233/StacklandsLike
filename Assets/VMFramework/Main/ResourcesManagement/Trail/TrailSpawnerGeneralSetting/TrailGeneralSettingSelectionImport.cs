#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class TrailGeneralSetting
    {
        private bool TryGetTrailRendererFromObject(Object obj, out TrailRenderer trailRenderer)
        {
            trailRenderer = null;

            if (obj is not GameObject gameObject)
            {
                return false;
            }

            if (gameObject.IsPrefabAsset() == false)
            {
                return false;
            }

            trailRenderer = gameObject.GetComponentInChildren<TrailRenderer>();

            return trailRenderer != null;
        }

        protected override void OnHandleRegisterGameItemPrefabFromSelectedObject(Object obj, bool checkUnique)
        {
            if (TryGetTrailRendererFromObject(obj, out var trailRenderer) == false)
            {
                return;
            }

            if (checkUnique)
            {
                if (GamePrefabManager.GetAllGamePrefabs<TrailPreset>()
                    .Any(prefab => prefab.trailPrefab == trailRenderer))
                {
                    Debug.LogWarning($"为了防止重复，忽略了{trailRenderer.name}的添加");
                    return;
                }
            }

            GamePrefabWrapperCreator.CreateGamePrefabWrapper(new TrailPreset()
            {
                id = trailRenderer.name.ToSnakeCase(),
                trailPrefab = trailRenderer,
            });
        }

        protected override void OnHandleUnregisterGameItemPrefabFromSelectedObject(Object obj)
        {
            if (TryGetTrailRendererFromObject(obj, out var trailRenderer) == false)
            {
                return;
            }

            GamePrefabWrapperRemover.RemoveGamePrefabWrapperWhere<TrailPreset>(prefab =>
                prefab.trailPrefab == trailRenderer);
        }

        protected override bool ShowRegisterGameItemPrefabFromSelectionButton(IEnumerable<Object> objects)
        {
            return objects.Any(obj =>
            {
                if (obj is GameObject gameObject)
                {
                    return gameObject.IsPrefabAsset() &&
                           gameObject.HasComponent<TrailRenderer>();
                }

                return false;
            });
        }
    }
}
#endif