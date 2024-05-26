using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class ToggleVisualElement : BasicVisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ToggleVisualElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasicVisualElement.UxmlTraits
        {
            UxmlBoolAttributeDescription _isChecked = new() { name = "IsChecked" };

            public override void Init(VisualElement ve, IUxmlAttributes bag,
                CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var toggleVisualElement = ve as ToggleVisualElement;

                toggleVisualElement.isChecked = _isChecked.GetValueFromBag(bag, cc);
            }
        }

        protected const string ussClassName = "toggle";

        protected const string borderUssClassName = ussClassName + "Border";

        protected const string checkmarkUssClassName = ussClassName + "Checkmark";

        protected VisualElement border;

        protected VisualElement checkmark;

        private bool _isChecked;

        public bool isChecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _isChecked;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => SetIsCheckedValue(value);
        }

        public event Action<bool> OnValueChanged;

        public ToggleVisualElement() : base()
        {
            border = new VisualElement();
            Add(border);
            border.AddToClassList(borderUssClassName);

            checkmark = new VisualElement();
            Add(checkmark);
            checkmark.AddToClassList(checkmarkUssClassName);

            isChecked = true;

            RegisterCallback<PointerDownEvent>(e => { isChecked = !isChecked; });
        }

        private void SetIsCheckedValue(bool newIsCheckedValue)
        {
            _isChecked = newIsCheckedValue;
            OnValueChanged?.Invoke(newIsCheckedValue);

            checkmark.style.visibility =
                isChecked ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
