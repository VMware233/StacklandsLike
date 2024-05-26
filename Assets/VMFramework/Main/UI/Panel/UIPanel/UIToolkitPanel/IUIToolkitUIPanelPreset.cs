using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public interface IUIToolkitUIPanelPreset : IUIPanelPreset
    {
        public PanelSettings panelSettings { get; }
        
        public VisualTreeAsset visualTree { get; }
    }
}