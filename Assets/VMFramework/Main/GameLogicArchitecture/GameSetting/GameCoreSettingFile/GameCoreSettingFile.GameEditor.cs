#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingFile : IGameEditorToolBarProvider, IGameEditorContextMenuProvider
    {
        IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> IGameEditorToolBarProvider.
            GetToolbarButtons()
        {
            yield return new(EditorNames.OPEN_SCRIPT_BUTTON, this.OpenScriptOfObject);
            yield return new(EditorNames.SAVE_BUTTON, this.EnforceSave);
        }
    }
}
#endif