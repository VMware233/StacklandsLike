using System;

namespace VMFramework.UI
{
    public interface IUIPanelPointerEventProvider : IUIPanelController
    {
        public void AddPointerEvent(Action<IUIPanelController> onPointerEnter,
            Action<IUIPanelController> onPointerLeave);
        
        public void RemovePointerEvent()
        {
            
        }
    }
}