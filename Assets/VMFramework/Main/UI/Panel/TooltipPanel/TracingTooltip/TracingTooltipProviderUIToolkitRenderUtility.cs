using System;
using VMFramework.Core;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public static class TracingTooltipProviderUIToolkitRenderUtility
    {
        public class RenderResult
        {
            public Dictionary<string, GroupVisualElement> groups;
            public List<DynamicPropertyInfo> dynamicPropertyInfos;
        }

        public struct DynamicPropertyInfo
        {
            public IconLabelVisualElement iconLabel;
            public Func<string> valueGetter;
        }

        public static RenderResult RenderToVisualElement(
            this ITooltipProvider tooltipProvider,
            Label titleLabel, Label descriptionLabel,
            VisualElement propertyContainer,
            Action<VisualElement, VisualElement> addVisualElementAction = null)
        {
            addVisualElementAction ??= (parent, child) => parent.Add(child);

            var titleText = tooltipProvider.GetTooltipTitle();

            if (titleText.IsNullOrEmpty())
            {
                titleLabel.style.display = DisplayStyle.None;
            }
            else
            {
                titleLabel.text = titleText;
            }

            var groups = new Dictionary<string, GroupVisualElement>();
            var dynamicPropertyInfos = new List<DynamicPropertyInfo>();
            var properties = tooltipProvider.GetTooltipProperties();

            if (properties != null)
            {
                foreach (var propertyConfig in properties)
                {
                    AddPropertyEntry(propertyConfig);
                }
            }

            var descriptionText = tooltipProvider.GetTooltipDescription();
            if (descriptionText.IsNullOrEmpty() == false)
            {
                descriptionLabel.text = descriptionText;
            }
            else
            {
                descriptionLabel.style.display = DisplayStyle.None;
            }

            var renderResult = new RenderResult()
            {
                groups = groups,
                dynamicPropertyInfos = dynamicPropertyInfos
            };

            return renderResult;

            void AddPropertyEntry(TooltipPropertyInfo propertyConfig)
            {
                var content = propertyConfig.attributeValueGetter();

                var propertyEntry = new IconLabelVisualElement();

                propertyEntry.SetIcon(propertyConfig.icon);
                propertyEntry.SetContent(content);

                if (propertyConfig.groupName.IsNullOrEmpty())
                {
                    addVisualElementAction(propertyContainer, propertyEntry);
                }
                else
                {
                    if (groups.TryGetValue(propertyConfig.groupName,
                            out var group) == false)
                    {
                        group = new GroupVisualElement()
                        {
                            name = propertyConfig.groupName
                        };

                        groups.Add(propertyConfig.groupName, group);

                        addVisualElementAction(propertyContainer, group);
                    }

                    addVisualElementAction(group, propertyEntry);
                }

                if (propertyConfig.isStatic == false)
                {
                    dynamicPropertyInfos.Add(new DynamicPropertyInfo()
                    {
                        iconLabel = propertyEntry,
                        valueGetter = propertyConfig.attributeValueGetter
                    });
                }
            }
        }
    }
}
