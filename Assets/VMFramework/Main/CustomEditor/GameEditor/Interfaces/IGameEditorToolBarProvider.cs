
using System;
using System.Collections.Generic;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorToolBarProvider
    {
        public struct ToolbarButtonConfig
        {
            public string path;
            public string tooltip;
            public Action onClick;
            
            public ToolbarButtonConfig(string path, Action onClick)
            {
                this.path = path;
                this.tooltip = path;
                this.onClick = onClick;
            }
            
            public ToolbarButtonConfig(string path, string tooltip, Action onClick)
            {
                this.path = path;
                this.tooltip = tooltip;
                this.onClick = onClick;
            }
        }

        public IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield break;
        }
    }
}