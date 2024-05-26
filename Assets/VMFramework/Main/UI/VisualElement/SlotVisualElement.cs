using System;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class SlotVisualElement : BasicVisualElement
    {
        public enum ContentContainerType
        {
            None,
            DeeperBackground,
            Background,
            Border,
            BorderContent,
            Icon,
            Description
        }

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SlotVisualElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasicVisualElement.UxmlTraits
        {
            private UxmlIntAttributeDescription _slotIndex = new()
            {
                name = "slot-index"
            };

            private UxmlBoolAttributeDescription _displayNoneIfNull = new()
            {
                name = "display-none-if-null"
            };

            private UxmlBoolAttributeDescription _ignoreBorderChange = new()
            {
                name = "ignore-border-change"
            };

            private UxmlBoolAttributeDescription _ignoreBackgroundChange = new()
            {
                name = "ignore-background-change"
            };

            private UxmlEnumAttributeDescription<ContentContainerType>
                _contentContainerType = new()
                {
                    name = "content-container-type"
                };

            private UxmlStringAttributeDescription _borderContentText = new()
            {
                name = "border-content-text"
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag,
                CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var slotVisualElement = ve as SlotVisualElement;

                slotVisualElement.slotIndex = _slotIndex.GetValueFromBag(bag, cc);
                slotVisualElement.displayNoneIfNull =
                    _displayNoneIfNull.GetValueFromBag(bag, cc);
                slotVisualElement.ignoreBorderChange =
                    _ignoreBorderChange.GetValueFromBag(bag, cc);
                slotVisualElement.ignoreBackgroundChange =
                    _ignoreBackgroundChange.GetValueFromBag(bag, cc);
                slotVisualElement.contentContainerType =
                    _contentContainerType.GetValueFromBag(bag, cc);
                slotVisualElement.borderContent.text =
                    _borderContentText.GetValueFromBag(bag, cc);
            }
        }

        private const string DEEPER_BACKGROUND_UI_NAME = "DeeperBackground";
        private const string BACKGROUND_UI_NAME = "Background";
        private const string BORDER_UI_NAME = "Border";
        private const string BORDER_CONTENT_UI_NAME = "BorderContent";
        private const string ICON_UI_NAME = "Icon";
        private const string COUNT_UI_NAME = "Description";

        private const string DEEPER_BACKGROUND_CLASS_STYLE = "slotDeeperBackground";
        private const string BACKGROUND_CLASS_STYLE = "slotBackground";
        private const string BORDER_CLASS_STYLE = "slotBorder";
        private const string BORDER_CONTENT_CLASS_STYLE = "slotBorderContent";
        private const string ICON_CLASS_STYLE = "slotIcon";
        private const string COUNT_CLASS_STYLE = "slotDescription";

        public VisualElement deeperBackground { get; }
        public VisualElement background { get; }
        public VisualElement border { get; }
        public Label borderContent { get; }
        public VisualElement icon { get; }
        public Label description { get; }

        public int slotIndex { get; set; }

        private bool _displayNoneIfNull;

        public bool displayNoneIfNull
        {
            get => _displayNoneIfNull;
            set
            {
                _displayNoneIfNull = value;
                if (currentSlotProvider == null)
                {
                    style.display = _displayNoneIfNull
                        ? DisplayStyle.None
                        : DisplayStyle.Flex;
                }
            }
        }

        public bool ignoreBorderChange { get; set; }
        public bool ignoreBackgroundChange { get; set; }

        public ContentContainerType contentContainerType { get; set; } =
            ContentContainerType.None;

        public override VisualElement contentContainer =>
            GetContentContainer(contentContainerType);

        public event Action OnLeftMouseClick;
        public event Action OnRightMouseClick;
        public event Action OnMiddleMouseClick;

        private ISlotProvider currentSlotProvider;

        private StyleBackground defaultBackgroundImage, defaultBorderImage;

        public SlotVisualElement() : base()
        {
            deeperBackground = new VisualElement
            {
                name = DEEPER_BACKGROUND_UI_NAME
            };
            background = new VisualElement
            {
                name = BACKGROUND_UI_NAME
            };
            border = new VisualElement
            {
                name = BORDER_UI_NAME
            };
            borderContent = new Label
            {
                name = BORDER_CONTENT_UI_NAME
            };
            icon = new VisualElement
            {
                name = ICON_UI_NAME
            };
            description = new Label
            {
                name = COUNT_UI_NAME,
                text = 64.ToString()
            };
            
            deeperBackground.AddToClassList(DEEPER_BACKGROUND_CLASS_STYLE);
            background.AddToClassList(BACKGROUND_CLASS_STYLE);
            border.AddToClassList(BORDER_CLASS_STYLE);
            borderContent.AddToClassList(BORDER_CONTENT_CLASS_STYLE);
            icon.AddToClassList(ICON_CLASS_STYLE);
            description.AddToClassList(COUNT_CLASS_STYLE);

            Add(deeperBackground);
            Add(background);
            Add(icon);
            Add(border);
            border.Add(borderContent);
            Add(description);

            RegisterCallback<MouseUpEvent>(e =>
            {
                if (e.button == 0)
                {
                    OnLeftMouseClick?.Invoke();
                }
                else if (e.button == 1)
                {
                    OnRightMouseClick?.Invoke();
                }
                else if (e.button == 2)
                {
                    OnMiddleMouseClick?.Invoke();
                }
            });

            OnMouseEnter += () =>
            {
                if (currentSlotProvider != null)
                {
                    currentSlotProvider.HandleMouseEnterEvent(source);
                }
            };

            OnMouseLeave += () =>
            {
                if (currentSlotProvider != null)
                {
                    currentSlotProvider.HandleMouseLeaveEvent(source);
                }
            };

            OnRightMouseClick += () =>
            {
                if (currentSlotProvider != null)
                {
                    currentSlotProvider.HandleRightMouseClickEvent(source);
                }
            };

            defaultBackgroundImage = background.style.backgroundImage;
            defaultBorderImage = border.style.backgroundImage;
        }

        public void SetSlotProvider(ISlotProvider slotProvider)
        {
            StyleBackground backgroundImage, borderImage;

            if (slotProvider == null)
            {
                backgroundImage = defaultBackgroundImage;
                borderImage = defaultBorderImage;

                icon.style.backgroundImage = null;
                description.text = "";

                if (ignoreBackgroundChange == false)
                {
                    background.style.backgroundImage = backgroundImage;
                }

                if (ignoreBorderChange == false)
                {
                    border.style.backgroundImage = borderImage;
                }

                if (displayNoneIfNull)
                {
                    style.display = DisplayStyle.None;
                }
                
                SetTooltip(default(ITooltipProvider));
            }
            else
            {
                style.display = DisplayStyle.Flex;

                if (slotProvider.enableBackgroundImageOverride)
                {
                    backgroundImage = slotProvider.GetBackgroundImage();

                    if (ignoreBackgroundChange == false)
                    {
                        background.style.backgroundImage = backgroundImage;
                    }
                }

                if (slotProvider.enableBorderImageOverride)
                {
                    borderImage = slotProvider.GetBorderImage();

                    if (ignoreBorderChange == false)
                    {
                        border.style.backgroundImage = borderImage;
                    }
                }

                icon.style.backgroundImage = slotProvider.GetIconImage();
                description.text = slotProvider.GetDescriptionText();
                
                SetTooltip(slotProvider as ITooltipProvider);
            }

            currentSlotProvider = slotProvider;
        }

        private VisualElement GetContentContainer(ContentContainerType contentContainerType)
        {
            return contentContainerType switch
            {
                ContentContainerType.DeeperBackground => deeperBackground,
                ContentContainerType.Background => background,
                ContentContainerType.Border => border,
                ContentContainerType.BorderContent => borderContent,
                ContentContainerType.Icon => icon,
                ContentContainerType.Description => description,
                _ => this
            };
        }
    }

    public interface ISlotProvider
    {
        public bool enableBackgroundImageOverride => false;

        public StyleBackground GetBackgroundImage() => null;

        public bool enableBorderImageOverride => false;

        public StyleBackground GetBorderImage() => null;

        public StyleBackground GetIconImage();

        public string GetDescriptionText();

        public void HandleRightMouseClickEvent(UIPanelController source)
        {

        }

        public void HandleMouseEnterEvent(UIPanelController source)
        {

        }

        public void HandleMouseLeaveEvent(UIPanelController source)
        {

        }
    }
}
