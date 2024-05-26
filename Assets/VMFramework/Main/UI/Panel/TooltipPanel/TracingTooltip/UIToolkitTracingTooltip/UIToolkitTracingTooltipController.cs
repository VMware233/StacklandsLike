using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class UIToolkitTracingTooltipController : UIToolkitTracingUIPanelController, ITracingTooltip
    {
        protected UIToolkitTracingTooltipPreset tracingTooltipPreset { get; private set; }
        
        [ShowInInspector]
        protected ITooltipProvider tooltipProvider { get; private set; }
        
        [ShowInInspector]
        protected TooltipOpenInfo currentOpenInfo { get; private set; }

        [ShowInInspector] 
        protected Label title, description;

        [ShowInInspector] 
        protected VisualElement propertyContainer;

        [ShowInInspector] 
        private List<TracingTooltipProviderUIToolkitRenderUtility.DynamicPropertyInfo> dynamicPropertyInfos = new();

        [ShowInInspector]
        private Dictionary<string, GroupVisualElement> groupVisualElements = new();

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            tracingTooltipPreset = preset as UIToolkitTracingTooltipPreset;

            tracingTooltipPreset.AssertIsNotNull(nameof(tracingTooltipPreset));
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            title = rootVisualElement.Q<Label>(tracingTooltipPreset.titleLabelName);
            description = rootVisualElement.Q<Label>(tracingTooltipPreset.descriptionLabelName);
            propertyContainer = rootVisualElement.Q(tracingTooltipPreset.propertyContainerName);

            title.AssertIsNotNull(nameof(title));
            description.AssertIsNotNull(nameof(description));
            propertyContainer.AssertIsNotNull(nameof(propertyContainer));

            dynamicPropertyInfos.Clear();

            groupVisualElements.Clear();
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            if (tooltipProvider != null)
            {
                if (tooltipProvider.TryGetTooltipBindGlobalEvent(out var gameEvent))
                {
                    gameEvent.OnEnabledChangedEvent -= OnGlobalEventEnabledStateChanged;
                }

                tooltipProvider = null;
            }

            title = null;
            description = null;
        }

        private void FixedUpdate()
        {
            if (isOpened && isClosing == false)
            {
                if (tooltipProvider.isDestroyed)
                {
                    this.Close();
                }
            }
            
            if (isOpened)
            {
                if (dynamicPropertyInfos.Count > 0)
                {
                    foreach (var attributeInfo in dynamicPropertyInfos)
                    {
                        attributeInfo.iconLabel.SetContent(attributeInfo.valueGetter());
                    }
                }
            }
        }

        private void OnGlobalEventEnabledStateChanged(bool previous, bool current)
        {
            if (current == false)
            {
                this.Close();
            }
        }

        public void Open(ITooltipProvider tooltipProvider, IUIPanelController source, TooltipOpenInfo info)
        {
            if (this.tooltipProvider == tooltipProvider)
            {
                return;
            }

            if (tooltipProvider.TryGetTooltipBindGlobalEvent(out var gameEvent))
            {
                if (gameEvent.isEnabled == false)
                {
                    return;
                }

                gameEvent.OnEnabledChangedEvent += OnGlobalEventEnabledStateChanged;
            }

            if (this.tooltipProvider != null)
            {
                if (info.priority < currentOpenInfo.priority)
                {
                    return;
                }
            }

            this.Open(source);

            this.tooltipProvider = tooltipProvider;
            currentOpenInfo = info;

            propertyContainer.Clear();

            var renderResult = tooltipProvider.RenderToVisualElement(title, description,
                propertyContainer, AddVisualElement);

            groupVisualElements = renderResult.groups;
            dynamicPropertyInfos = renderResult.dynamicPropertyInfos;
        }

        public void Close(ITooltipProvider tooltipProvider)
        {
            if (isClosing)
            {
                Debug.LogWarning("Tooltip is already closing.");
                return;
            }
            
            if (this.tooltipProvider == tooltipProvider)
            {
                this.Close();
            }
        }
    }
}
