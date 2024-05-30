#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.UI
{
    public partial class UIPanelPreset : IGameEditorToolBarProvider
    {
        public IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.OPEN_CONTROLLER_SCRIPT_BUTTON_PATH, OpenControllerScript);
        }
    }
}
#endif