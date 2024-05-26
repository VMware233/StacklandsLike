#if DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration.Animation
{
    [LabelText("跳跃动画")]
    public partial class Leap : GameObjectAnimationClip
    {
        [LabelText("跳跃持续时间")]
        [MinValue(0)]
        public float leapDuration = 0.7f;

        [LabelText("跳跃落点")]
        public IChooserConfig<Vector3> leapEndOffset = new SingleValueChooserConfig<Vector3>();

        [LabelText("跳跃力量")]
        [MinValue(0)]
        public IChooserConfig<float> leapPower = new SingleValueChooserConfig<float>();

        [LabelText("跳跃次数")]
        [MinValue(0)]
        public IChooserConfig<int> leapTimes = new SingleValueChooserConfig<int>(1);

        public override float GetDuration() => leapDuration;

        public override async UniTask Run(Transform target, CancellationToken token)
        {
            await base.Run(target, token);
            
            await target.DOLocalJump(target.localPosition + leapEndOffset.GetValue(),
                    leapPower.GetValue(), leapTimes.GetValue(), leapDuration, false)
                .AwaitForComplete(cancellationToken: token);
        }

        public override void Kill(Transform target)
        {
            target.DOKill();
        }

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            leapEndOffset.CheckSettings();
            leapPower.CheckSettings();
            leapTimes.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            leapEndOffset.Init();
            leapPower.Init();
            leapTimes.Init();
        }
    }
}
#endif