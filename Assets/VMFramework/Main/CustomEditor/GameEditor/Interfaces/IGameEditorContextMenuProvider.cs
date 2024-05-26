using System;
using System.Collections.Generic;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorContextMenuProvider
    {
        public struct MenuItemConfig
        {
            public string name;
            public string tooltip;
            public Action onClick;
            
            public MenuItemConfig(string name, Action onClick)
            {
                this.name = name;
                this.tooltip = name;
                this.onClick = onClick;
            }
            
            public MenuItemConfig(string name, string tooltip, Action onClick)
            {
                this.name = name;
                this.tooltip = tooltip;
                this.onClick = onClick;
            }
        }
        
        public IEnumerable<MenuItemConfig> GetMenuItems()
        {
            if (this is IGameEditorToolBarProvider toolBarProvider)
            {
                foreach (var config in toolBarProvider.GetToolbarButtons())
                {
                    yield return new(config.name, config.tooltip, config.onClick);
                }
            }
        }
    }
}