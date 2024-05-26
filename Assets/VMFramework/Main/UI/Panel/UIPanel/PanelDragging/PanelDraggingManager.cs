using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class PanelDraggingManager : UniqueMonoBehaviour<PanelDraggingManager>
    {
        [ShowInInspector]
        private static readonly HashSet<IDraggablePanel> draggingPanels = new();

        [ShowInInspector]
        private static readonly List<IDraggablePanel> removalDraggingPanels = new();

        private static Vector2 lastMousePosition;

        protected override void Awake()
        {
            base.Awake();

            UIPanelManager.OnPanelCreatedEvent += OnPanelCreated;
        }

        private void Start()
        {
            lastMousePosition = Input.mousePosition;
        }

        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            foreach (var draggablePanel in draggingPanels)
            {
                draggablePanel.OnDrag(mousePosition - lastMousePosition,
                    mousePosition);
            }

            foreach (var draggablePanel in removalDraggingPanels)
            {
                if (draggingPanels.Remove(draggablePanel))
                {
                    draggablePanel.OnDragStop();
                }
            }

            removalDraggingPanels.Clear();

            lastMousePosition = mousePosition;
        }

        private static void OnPanelCreated(IUIPanelController panelController)
        {
            if (panelController is not IDraggablePanel draggablePanel)
            {
                return;
            }

            panelController.OnOpenInstantlyEvent += OnPanelOpened;
            panelController.OnCloseInstantlyEvent += OnPanelClosed;
        }

        private static void OnPanelOpened(IUIPanelController panelController)
        {
            if (panelController is not IDraggablePanel draggablePanel)
            {
                return;
            }

            draggablePanel.AddMouseDownListener(StartDrag);
            draggablePanel.AddMouseUpListener(StopDrag);
            draggablePanel.AddMouseLeaveListener(StopDrag);
        }

        private static void OnPanelClosed(IUIPanelController panelController)
        {

        }

        private static void StartDrag(IDraggablePanel draggablePanel)
        {
            if (draggablePanel.isDraggable)
            {
                draggingPanels.Add(draggablePanel);
                draggablePanel.OnDragStart();
            }
        }

        public static void StopDrag(IDraggablePanel draggablePanel)
        {
            removalDraggingPanels.Add(draggablePanel);
        }
    }
}
