#if UNITY_EDITOR && DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration.Animation
{
    public partial class GameObjectAnimation
    {
        [FoldoutGroup(PRESET_CATEGORY)]
        [Button("跳跃淡出预设")]
        private void AddLeapFadeOutPreset()
        {
            clips ??= new();

            clips.Add(new FadeOut()
            {
                startTime = 0.3f,
                fadeDuration = 0.25f,
                setAlphaToOneOnStart = true
            });

            clips.Add(new Leap()
            {
                startTime = 0,
                leapDuration = 0.7f,
                leapPower = new SingleValueChooserConfig<float>(50),
                leapTimes = new SingleValueChooserConfig<int>(2),
                leapEndOffset = new WeightedSelectChooserConfig<Vector3>(new Vector3[]
                {
                    new Vector2(140, 50),
                    new Vector2(-140, 50)
                })
            });
        }

        [FoldoutGroup(PRESET_CATEGORY)]
        [Button("从底下浮现并淡出预设")]
        private void AddRiseAndFadeOutPreset()
        {
            clips ??= new();

            clips.Add(new FadeIn()
            {
                startTime = 0,
                fadeDuration = 0.25f,
                setAlphaToZeroOnStart = true,
            });

            clips.Add(new Move()
            {
                startTime = 0,
                moveDuration = 0.3f,
                end = new SingleValueChooserConfig<Vector3>(new Vector2(0, 65)),
                ease = Ease.OutCubic
            });

            clips.Add(new FadeOut()
            {
                startTime = 0.3f,
                fadeDuration = 0.2f,
                setAlphaToOneOnStart = false
            });
        }
    }
}
#endif