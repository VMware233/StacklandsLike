using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;

namespace VMFramework.UI
{
    public class DraggableUIToolkitPanelController : UIToolkitPanelController, IDraggableUIToolkitPanel
    {
        protected DraggableUIToolkitPanelPreset draggableUIToolkitPanelPreset { get; private set; }

        [ShowInInspector]
        private VisualElement draggableArea;

        [ShowInInspector]
        private VisualElement draggingContainer;

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            draggableUIToolkitPanelPreset = preset as DraggableUIToolkitPanelPreset;

            draggableUIToolkitPanelPreset.AssertIsNotNull(nameof(draggableUIToolkitPanelPreset));
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            if (draggableUIToolkitPanelPreset.draggableAreaName.IsNullOrEmpty() == false)
            {
                draggableArea = rootVisualElement.QueryStrictly(
                    draggableUIToolkitPanelPreset.draggableAreaName,
                    nameof(draggableUIToolkitPanelPreset.draggableAreaName));
            }

            if (draggableUIToolkitPanelPreset.draggingContainerName.IsNullOrEmpty() == false)
            {
                draggingContainer = rootVisualElement.QueryStrictly(
                    draggableUIToolkitPanelPreset.draggingContainerName,
                    nameof(draggableUIToolkitPanelPreset.draggingContainerName));
            }
        }

        bool IDraggablePanel.enableDragging => draggableUIToolkitPanelPreset.enableDragging;

        void IDraggablePanel.OnDragStart()
        {
            if (preset.isDebugging)
            {
                Debug.LogWarning($"开始拖拽:{this.name}");
            }
        }

        void IDraggablePanel.OnDragStop()
        {
            if (preset.isDebugging)
            {
                Debug.LogWarning($"结束拖拽:{this.name}");
            }
        }

        Vector2 IDraggableUIToolkitPanel.referenceResolution => 
            uiDocument.panelSettings.referenceResolution;

        bool IDraggableUIToolkitPanel.draggableOverflowScreen =>
            draggableUIToolkitPanelPreset.draggableOverflowScreen;

        VisualElement IDraggableUIToolkitPanel.draggableArea => draggableArea;

        VisualElement IDraggableUIToolkitPanel.draggingContainer => draggingContainer;
    }
}