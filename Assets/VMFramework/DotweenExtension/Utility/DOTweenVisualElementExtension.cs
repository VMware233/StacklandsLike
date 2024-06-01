#if DOTWEEN
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace VMFramework.DOTweenExtension
{
    public static class DOTweenVisualElementExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DOKill(this VisualElement visualElement)
        {
            DOTween.Kill(visualElement);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenerCore<float, float, FloatOptions> DOFade(
            this VisualElement visualElement, float endValue, float duration)
        {
            var t = DOTween.To(() => visualElement.style.opacity.value,
                opacityValue => visualElement.style.opacity = opacityValue,
                endValue, duration);
            t.SetTarget(visualElement);
            return t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TweenerCore<float, float, FloatOptions> DOLeft(this VisualElement visualElement,
            float endValue, float duration)
        {
            var t = DOTween.To(() => visualElement.style.left.value.value,
                leftValue => visualElement.style.left = leftValue, endValue, duration);
            t.SetTarget(visualElement);
            return t;
        }
    }
}

#endif