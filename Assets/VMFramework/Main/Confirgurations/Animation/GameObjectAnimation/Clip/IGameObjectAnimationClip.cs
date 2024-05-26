using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VMFramework.Configuration.Animation
{
    public interface IGameObjectAnimationClip : IConfig
    {
        public float GetStartTime();
        
        public float GetDuration();

        public bool IsRequirementSatisfied(Transform target);

        public UniTask Run(Transform target, CancellationToken token);

        public void Kill(Transform target);

        public void Start(Transform target);
    }
}