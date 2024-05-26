using System;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.ResourcesManagement
{
    public sealed partial class ParticleGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(ParticlePreset);

        #endregion

        [HideLabel, TabGroup(TAB_GROUP_NAME, MISCELLANEOUS_CATEGORY)]
        public ContainerChooser container = new();

        [Button("设置持续时间"), TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        public void SetDuration(
            [GamePrefabID(typeof(ParticlePreset))]
            string id, 
            [MinValue(0)]
            float duration)
        {
            var prefab = GamePrefabManager.GetGamePrefabStrictly<ParticlePreset>(id);

            foreach (var particleSystem in
                     prefab.particlePrefab.QueryComponentsInChildren<ParticleSystem>(true))
            {
                var main = particleSystem.main;
                main.duration = duration;
            }
        }
    }
}