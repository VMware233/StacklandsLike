using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration.Animation
{
    public abstract class GameObjectAnimationClip : BaseConfig, IGameObjectAnimationClip
    {
        [LabelText("开始时间")]
        [MinValue(0)]
        public float startTime = 0;
        
        public float GetStartTime() => startTime;
        
        public abstract float GetDuration();

        public virtual bool IsRequirementSatisfied(Transform target)
        {
            return true;
        }

        public virtual async UniTask Run(Transform target, CancellationToken token)
        {
            await UniTask.WaitForSeconds(startTime, cancellationToken: token);
        }
        
        public abstract void Kill(Transform target);

        public virtual void Start(Transform target)
        {
            
        }
    }
}