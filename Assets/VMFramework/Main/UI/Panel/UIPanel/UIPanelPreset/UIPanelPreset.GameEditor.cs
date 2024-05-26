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
            yield return new(EditorNames.openControllerScriptButtonName, OpenControllerScript);
        }
    }
}
#endif