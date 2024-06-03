using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    [UxmlElement]
    public partial class BasicToggle : VisualElement
    {
        public const string ussClassName = "toggle";
        public const string inputContainerUssClassName = "toggle-input-container";
        public const string titleUssClassName = "toggle-title";
        public const string checkMarkUssClassName = "toggle-checkmark";
        public const string checkmarkIconUssClassName = "toggle-checkmark-icon";
        public const string enabledCheckmarkIconUssClassName = "toggle-enabled-checkmark-icon";
        public const string disabledCheckmarkIconUssClassName = "toggle-disabled-checkmark-icon";
        public const string contentUssClassName = "toggle-content";

        public Label titleLabel { get; private set; }
        
        public VisualElement inputContainer { get; private set; }

        public VisualElement checkmark { get; private set; }
        
        public VisualElement enabledCheckmarkIcon { get; private set; }
        
        public VisualElement disabledCheckmarkIcon { get; private set; }
        
        public Label contentLabel { get; private set; }

        private bool _isOn;

        [UxmlAttribute]
        public bool isOn
        {
            get => _isOn;
            set
            {
                _isOn = value;

                if (_isOn)
                {
                    enabledCheckmarkIcon.style.display = DisplayStyle.Flex;
                    disabledCheckmarkIcon.style.display = DisplayStyle.None;
                }
                else
                {
                    enabledCheckmarkIcon.style.display = DisplayStyle.None;
                    disabledCheckmarkIcon.style.display = DisplayStyle.Flex;
                }
            }
        }

        [UxmlAttribute]
        public string title {
            get => titleLabel.text;
            set => titleLabel.text = value;
        }
        
        [UxmlAttribute]
        public string content
        {
            get => contentLabel.text;
            set => contentLabel.text = value;
        }
        
        [UxmlAttribute]
        public bool isReadOnly { get; set; }

        private Clickable clickable;

        public BasicToggle() : base()
        {
            AddToClassList(ussClassName);
            
            titleLabel = new Label();
            titleLabel.AddToClassList(titleUssClassName);
            Add(titleLabel);
            
            inputContainer = new VisualElement();
            inputContainer.AddToClassList(inputContainerUssClassName);
            Add(inputContainer);
            
            checkmark = new VisualElement();
            checkmark.AddToClassList(checkMarkUssClassName);
            inputContainer.Add(checkmark);
            
            enabledCheckmarkIcon = new VisualElement();
            enabledCheckmarkIcon.AddToClassList(enabledCheckmarkIconUssClassName);
            enabledCheckmarkIcon.AddToClassList(checkmarkIconUssClassName);
            checkmark.Add(enabledCheckmarkIcon);
            
            disabledCheckmarkIcon = new VisualElement();
            disabledCheckmarkIcon.AddToClassList(disabledCheckmarkIconUssClassName);
            disabledCheckmarkIcon.AddToClassList(checkmarkIconUssClassName);
            checkmark.Add(disabledCheckmarkIcon);
            
            contentLabel = new Label();
            contentLabel.AddToClassList(contentUssClassName);
            inputContainer.Add(contentLabel);

            title = "Title";
            content = "Content";

            isOn = false;

            // clickable = new Clickable(OnClickEvent);
            // this.AddManipulator(clickable);
        }

        private void OnClickEvent(EventBase e)
        {
            Debug.LogError(e.eventTypeId);
            
            if (isReadOnly)
            {
                return;
            }

            isOn = !isOn;
        }
    }
}