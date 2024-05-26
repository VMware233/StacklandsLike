using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public sealed class IconLabelVisualElement : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<IconLabelVisualElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlBoolAttributeDescription _iconAlwaysDisplay =
                new() { name = "IconAlwaysDisplay" };


            public override void Init(VisualElement ve, IUxmlAttributes bag,
                CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var iconLabel = ve as IconLabelVisualElement;

                iconLabel.iconAlwaysDisplay =
                    _iconAlwaysDisplay.GetValueFromBag(bag, cc);
            }
        }

        [ShowInInspector]
        public bool iconAlwaysDisplay { get; set; } = false;

        public VisualElement icon { get; }
        public Label label { get; }

        public IconLabelVisualElement() : base()
        {
            icon = new VisualElement
            {
                name = "Icon"
            };
            icon.AddToClassList("icon");
            Add(icon);

            label = new Label
            {
                name = "Content",
                text = "Label:Content"
            };
            label.AddToClassList("content");
            Add(label);
            
            AddToClassList("iconLabel");
        }

        public void SetIcon(Sprite icon)
        {
            if (iconAlwaysDisplay == false)
            {
                if (icon == null)
                {
                    this.icon.style.display = DisplayStyle.None;
                }
                else
                {
                    this.icon.style.display = DisplayStyle.Flex;
                }
            }

            this.icon.style.backgroundImage = new StyleBackground(icon);
        }

        public void SetContent(string content)
        {
            label.text = content;
        }
    }
}
