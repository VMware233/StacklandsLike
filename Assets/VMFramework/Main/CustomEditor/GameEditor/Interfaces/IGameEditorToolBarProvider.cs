
using System;
using System.Collections.Generic;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorToolBarProvider
    {
        public struct ToolbarButtonConfig
        {
            public string name;
            public string tooltip;
            public Action onClick;
            
            public ToolbarButtonConfig(string name, Action onClick)
            {
                this.name = name;
                this.tooltip = name;
                this.onClick = onClick;
            }
            
            public ToolbarButtonConfig(string name, string tooltip, Action onClick)
            {
                this.name = name;
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