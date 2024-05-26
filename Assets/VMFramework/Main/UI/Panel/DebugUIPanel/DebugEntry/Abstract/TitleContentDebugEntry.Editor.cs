#if UNITY_EDITOR
using UnityEngine;

namespace VMFramework.UI
{
    public partial class TitleContentDebugEntry
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();
            
            titleFormat ??= new()
            {
                overrideFontColor = true,
                fontColor = Color.white
            };
            contentFormat ??= new()
            {
                overrideFontColor = true,
                fontColor = Color.white
            };
        }
    }
}
#endif