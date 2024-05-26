#if UNITY_EDITOR

using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GeneralSetting : IGameEditorToolBarProvider, IGameEditorMenuTreeNode, 
        IGameEditorContextMenuProvider
    {
        public static LocalizedTempString settingSuffixName = new()
        {
            { "en-US", "General Setting" },
            { "zh-CN", "通用设置" }
        };
        
        protected virtual IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.openScriptButtonName, this.OpenScriptOfObject);
            yield return new(EditorNames.saveButtonName, this.EnforceSave);
        }

        IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> IGameEditorToolBarProvider.GetToolbarButtons()
        {
            return GetToolbarButtons();
        }
    }
}

#endif