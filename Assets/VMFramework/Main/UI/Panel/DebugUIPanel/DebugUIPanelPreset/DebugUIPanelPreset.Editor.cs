#if UNITY_EDITOR
using UnityEngine.UIElements;
using VMFramework.Core.Editor;

namespace VMFramework.UI
{
    public partial class DebugUIPanelPreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            name.defaultValue = "Debug Screen";

            if (visualTree == null)
            {
                visualTree = DEBUGGING_SCREEN_VISUAL_TREE_ASSET_NAME.FindAssetOfName<VisualTreeAsset>();
            }

            ignoreMouseEvents = true;
        }
    }
}
#endif