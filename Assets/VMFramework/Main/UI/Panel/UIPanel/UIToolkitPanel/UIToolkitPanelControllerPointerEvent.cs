using System;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public partial class UIToolkitPanelController : IUIPanelPointerEventProvider
    {
        private Action<IUIPanelController> OnPointerEnterEvent;
        private Action<IUIPanelController> OnPointerLeaveEvent;

        void IUIPanelPointerEventProvider.AddPointerEvent(Action<IUIPanelController> onPointerEnter,
            Action<IUIPanelController> onPointerLeave)
        {
            OnPointerEnterEvent = onPointerEnter;
            OnPointerLeaveEvent = onPointerLeave;
            
            foreach (var visualElement in rootVisualElement.Children())
            {
                visualElement.RegisterCallback<MouseEnterEvent>(OnPointerEnter);
                visualElement.RegisterCallback<MouseLeaveEvent>(OnPointerLeave);
            }
        }

        void IUIPanelPointerEventProvider.RemovePointerEvent()
        {
            OnPointerEnterEvent = null;
            OnPointerLeaveEvent = null;
            
            foreach (var visualElement in rootVisualElement.Children())
            {
                visualElement.UnregisterCallback<MouseEnterEvent>(OnPointerEnter);
                visualElement.UnregisterCallback<MouseLeaveEvent>(OnPointerLeave);
            }
        }

        private void OnPointerEnter(MouseEnterEvent e)
        {
            OnPointerEnterEvent?.Invoke(this);
        }
        
        private void OnPointerLeave(MouseLeaveEvent e)
        {
            OnPointerLeaveEvent?.Invoke(this);
        }
    }
}