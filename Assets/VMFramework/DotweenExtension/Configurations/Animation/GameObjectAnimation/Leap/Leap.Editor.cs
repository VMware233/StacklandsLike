#if UNITY_EDITOR && DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.Configuration.Animation
{
    public partial class Leap
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            leapEndOffset ??= new SingleValueChooserConfig<Vector3>();
            leapPower ??= new SingleValueChooserConfig<float>();
            leapTimes ??= new SingleValueChooserConfig<int>(1);
        }
    }
}
#endif