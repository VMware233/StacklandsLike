#if DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration.Animation
{
    public abstract class Fade : GameObjectAnimationClip
    {
        [LabelText("淡化时间")]
        [MinValue(0)]
        public float fadeDuration = 0.25f;

        public override float GetDuration() => fadeDuration;

        public override bool IsRequirementSatisfied(Transform target)
        {
            if (target.GetComponent<CanvasGroup>() == null)
            {
                Debug.LogWarning($"{target}没有CanvasGroup组件，无法播放淡化动画");
                return false;
            }

            return true;
        }

        public sealed override void Start(Transform target)
        {
            base.Start(target);
            
            var canvasGroup = target.GetComponent<CanvasGroup>();

            OnStart(canvasGroup);
        }

        public sealed override async UniTask Run(Transform target, CancellationToken token)
        {
            await base.Run(target, token);
            
            var canvasGroup = target.GetComponent<CanvasGroup>();

            await Run(canvasGroup, token);
        }

        public sealed override void Kill(Transform target)
        {
            var canvasGroup = target.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
        }

        protected abstract UniTask Run(CanvasGroup canvasGroup, CancellationToken token);

        protected abstract void OnStart(CanvasGroup canvasGroup);
    }
}
#endif