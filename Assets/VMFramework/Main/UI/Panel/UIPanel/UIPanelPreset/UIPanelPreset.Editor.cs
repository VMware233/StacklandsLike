#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core.Editor;

namespace VMFramework.UI
{
    public partial class UIPanelPreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            gameEventDisabledOnOpen ??= new();
        }

        [Button, TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        private void OpenControllerScript()
        {
            controllerType.OpenScriptOfType();
        }
    }
}
#endif