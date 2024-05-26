#if DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration.Animation
{
    [LabelText("淡入动画")]
    public class FadeIn : Fade
    {
        [LabelText("开始时设置透明度为0")]
        [LabelWidth(180)]
        [ToggleButtons("是", "否")]
        public bool setAlphaToZeroOnStart = false;

        protected override UniTask Run(CanvasGroup canvasGroup, CancellationToken token)
        {
            return canvasGroup.DOFade(1, fadeDuration).AwaitForComplete(cancellationToken: token);
        }

        protected override void OnStart(CanvasGroup canvasGroup)
        {
            if (setAlphaToZeroOnStart)
            {
                canvasGroup.alpha = 0;
            }
        }
    }
}
#endif