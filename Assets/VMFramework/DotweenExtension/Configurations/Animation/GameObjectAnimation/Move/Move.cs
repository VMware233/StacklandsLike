#if DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration.Animation
{
    [LabelText("移动动画")]
    public partial class Move : GameObjectAnimationClip
    {
        [LabelText("移动持续时间")]
        [MinValue(0)]
        public float moveDuration = 0.3f;

        [LabelText("终点")]
        public IChooserConfig<Vector3> end = new SingleValueChooserConfig<Vector3>();

        [LabelText("动画曲线")]
        [Helper("https://easings.net/")]
        public Ease ease = Ease.Linear;

        public override float GetDuration() => moveDuration;

        public override async UniTask Run(Transform target, CancellationToken token)
        {
            await base.Run(target, token);
            
            await target.DOLocalMove(target.localPosition + end.GetValue(), moveDuration).SetEase(ease)
                .AwaitForComplete(cancellationToken: token);
        }

        public override void Kill(Transform target)
        {
            target.DOKill();
        }

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            end.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            end.Init();
        }
    }
}
#endif