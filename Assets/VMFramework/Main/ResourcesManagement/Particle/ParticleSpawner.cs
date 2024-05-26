using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Pool;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    [ManagerCreationProvider(ManagerType.ResourcesCore)]
    public class ParticleSpawner : SerializedMonoBehaviour
    {
        private static readonly Dictionary<string, IComponentPool<ParticleSystem>> allPools =
            new();

        private static readonly Dictionary<ParticleSystem, string> allParticleIDs = new();

        private static IComponentPool<ParticleSystem> CreatePool(string id)
        {
            return new StackComponentPool<ParticleSystem>(() =>
            {
                var registeredParticle = GamePrefabManager.GetGamePrefabStrictly<ParticlePreset>(id);
                var prefab = registeredParticle.particlePrefab;
                var particleSystem = Instantiate(prefab, GameCoreSetting.particleGeneralSetting.container);
                return particleSystem;
            }, onReturnCallback: particle =>
            {
                particle.SetActive(false);
                particle.transform.SetParent(GameCoreSetting.particleGeneralSetting.container);
            });
        }
        
        /// <summary>
        /// 回收粒子
        /// </summary>
        /// <param name="particle"></param>
        public static void Return(ParticleSystem particle)
        {
            if (particle == null)
            {
                return;
            }
            
            if (particle.gameObject.activeSelf)
            {
                var id = allParticleIDs[particle];
                var pool = allPools[id];
                
                pool.Return(particle);
            }
        }

        /// <summary>
        /// 生成粒子
        /// 如果父Transform为Null，则为位置参数为world space position，如若不然，则是local position
        /// </summary>
        /// <param name="id">粒子ID</param>
        /// <param name="pos">位置</param>
        /// <param name="parent">父Transform</param>
        /// <param name="isWorldSpace"></param>
        /// <returns></returns>
        [Button("生成粒子")]
        public static ParticleSystem Spawn(
            [GamePrefabID(typeof(ParticlePreset))]
            string id, Vector3 pos, Transform parent = null, bool isWorldSpace = true)
        {
            var registeredParticle = GamePrefabManager.GetGamePrefabStrictly<ParticlePreset>(id);
            
            if (allPools.TryGetValue(id, out var pool) == false)
            {
                pool = CreatePool(id);
                allPools[id] = pool;
            }

            var newParticleSystem = pool.Get(parent);

            allParticleIDs[newParticleSystem] = id;

            if (isWorldSpace)
            {
                newParticleSystem.transform.position = pos;
            }
            else
            {
                newParticleSystem.transform.localPosition = pos;
            }

            newParticleSystem.Clear();
            newParticleSystem.Stop();

            1.DelayFrameAction(() => { newParticleSystem.Play(); });

            if (registeredParticle.enableDurationLimitation)
            {
                registeredParticle.duration.GetValue().DelayAction(() =>
                {
                    Return(newParticleSystem);
                });
            }

            return newParticleSystem;
        }

        [Button("设置持续时间")]
        public static void SetDuration(
            [GamePrefabID(typeof(ParticlePreset))]
            string id, float duration)
        {
            GameCoreSetting.particleGeneralSetting.SetDuration(id,
                duration);
        }
    }
}
