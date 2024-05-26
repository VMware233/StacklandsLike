#if DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration.Animation
{
    [LabelText("淡出动画")]
    public class FadeOut : Fade
    {
        [LabelText("开始时设置透明度为1")]
        [LabelWidth(180)]
        [ToggleButtons("是", "否")]
        public bool setAlphaToOneOnStart = false;

        protected override UniTask Run(CanvasGroup canvasGroup, CancellationToken token)
        {
            return canvasGroup.DOFade(0, fadeDuration).AwaitForComplete(cancellationToken: token);
        }

        protected override void OnStart(CanvasGroup canvasGroup)
        {
            if (setAlphaToOneOnStart)
            {
                canvasGroup.alpha = 1;
            }
        }
    }
}
#endif