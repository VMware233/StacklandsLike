using VMFramework.Core;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public interface IDraggablePanel
    {
        public bool enableDragging { get; }

        public bool isDraggable { get; }

        public void AddMouseDownListener(Action<IDraggablePanel> onMouseDown);

        public void AddMouseUpListener(Action<IDraggablePanel> onMouseUp);

        public void AddMouseLeaveListener(Action<IDraggablePanel> onMouseLeave);

        void OnDragStart();

        void OnDrag(Vector2 mouseDelta, Vector2 mousePosition);

        void OnDragStop();
    }

    public interface IDraggableUIToolkitPanel : IDraggablePanel
    {
        protected Vector2 referenceResolution { get; }

        protected bool draggableOverflowScreen { get; }

        protected VisualElement draggableArea { get; }

        protected VisualElement draggingContainer { get; }

        bool IDraggablePanel.isDraggable => draggableArea != null;

        void IDraggablePanel.AddMouseUpListener(Action<IDraggablePanel> onMouseUp)
        {
            if (enableDragging == false)
            {
                return;
            }

            draggableArea.RegisterCallback<MouseUpEvent>(_ => onMouseUp(this));
        }

        void IDraggablePanel.AddMouseDownListener(Action<IDraggablePanel> onMouseDown)
        {
            if (enableDragging == false)
            {
                return;
            }

            draggableArea.RegisterCallback<MouseDownEvent>(_ => onMouseDown(this));
        }

        void IDraggablePanel.AddMouseLeaveListener(Action<IDraggablePanel> onMouseLeave)
        {
            if (enableDragging == false)
            {
                return;
            }

            draggableArea.RegisterCallback<MouseLeaveEvent>(_ => onMouseLeave(this));
        }

        void IDraggablePanel.OnDrag(Vector2 mouseDelta, Vector2 mousePosition)
        {
            Vector2 screenSize = new(Screen.width, Screen.height);

            if (mousePosition.IsOverflow(Vector2.zero, screenSize))
            {
                PanelDraggingManager.StopDrag(this);
            }

            Vector2 boundsSize = referenceResolution;

            Vector2 delta = mouseDelta.Divide(screenSize).Multiply(boundsSize);

            var left = draggingContainer.resolvedStyle.left;
            var bottom = -draggingContainer.resolvedStyle.bottom;

            var width = draggingContainer.resolvedStyle.width;
            var height = draggingContainer.resolvedStyle.height;

            var resultLeft = left + delta.x;
            var resultBottom = bottom + delta.y;

            if (draggableOverflowScreen == false)
            {
                resultLeft = resultLeft.Clamp(0, boundsSize.x - width);
                resultBottom = resultBottom.Clamp(0, boundsSize.y - height);
            }

            draggingContainer.SetPosition(new Vector2(resultLeft, resultBottom));
        }
    }
}
