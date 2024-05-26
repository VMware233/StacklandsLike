#if UNITY_EDITOR && DOTWEEN && UNITASK_DOTWEEN_SUPPORT
using UnityEngine;

namespace VMFramework.Configuration.Animation
{
    public partial class Move
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            end ??= new SingleVectorChooserConfig<Vector3>();
        }
    }
}
#endif