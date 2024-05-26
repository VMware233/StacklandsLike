#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class ParticleGeneralSetting
    {
        private bool TryGetParticleSystemFromObject(Object obj,
            out ParticleSystem particleSystem)
        {
            particleSystem = null;

            if (obj is not GameObject gameObject)
            {
                return false;
            }

            if (gameObject.IsPrefabAsset() == false)
            {
                return false;
            }

            particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();

            return particleSystem != null;
        }

        protected override void OnHandleRegisterGameItemPrefabFromSelectedObject(
            Object obj, bool checkUnique)
        {
            if (TryGetParticleSystemFromObject(obj, out var particleSystem) == false)
            {
                return;
            }

            if (checkUnique)
            {
                if (GamePrefabManager.GetAllGamePrefabs<ParticlePreset>()
                    .Any(prefab => prefab.particlePrefab == particleSystem))
                {
                    Debug.LogWarning($"为了防止重复，忽略了{particleSystem.name}的添加");
                    return;
                }
            }

            var enableDurationLimitation = particleSystem.main.loop == false;

            var duration = particleSystem.main.duration;

            GamePrefabWrapperCreator.CreateGamePrefabWrapper(new ParticlePreset()
            {
                id = particleSystem.name.ToSnakeCase(),
                particlePrefab = particleSystem,
                enableDurationLimitation = enableDurationLimitation,
                duration = new SingleValueChooserConfig<float>(duration)
            });
        }

        protected override void OnHandleUnregisterGameItemPrefabFromSelectedObject(Object obj)
        {
            if (TryGetParticleSystemFromObject(obj, out var particleSystem) == false)
            {
                return;
            }

            GamePrefabWrapperRemover.RemoveGamePrefabWrapperWhere<ParticlePreset>(prefab =>
                prefab.particlePrefab == particleSystem);
        }

        protected override bool ShowRegisterGameItemPrefabFromSelectionButton(
            IEnumerable<Object> objects)
        {
            return objects.Any(obj =>
            {
                if (obj is GameObject gameObject)
                {
                    return gameObject.IsPrefabAsset() &&
                           gameObject.HasComponent<ParticleSystem>();
                }

                return false;
            });
        }
    }
}
#endif