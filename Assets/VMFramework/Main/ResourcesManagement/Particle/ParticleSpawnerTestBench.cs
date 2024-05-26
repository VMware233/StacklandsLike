using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.ResourcesManagement
{
    public class ParticleSpawnerTestBench : MonoBehaviour
    {
        [GamePrefabID(typeof(ParticlePreset))]
        public string id;

        public Vector3 pos;

        [Button(nameof(Spawn))]
        public void Spawn()
        {
            ParticleSpawner.Spawn(id, pos);
        }

        public Sprite sprite;

        [Button(nameof(SpawnPixelDestroy))]
        public void SpawnPixelDestroy()
        {
            //ParticleSpawner.SpawnPixelDestroyParticle(pos, sprite);
        }

    }
}
