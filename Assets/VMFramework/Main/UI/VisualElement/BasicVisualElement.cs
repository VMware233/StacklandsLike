using System;
using Sirenix.OdinInspector;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public class BasicVisualElement : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<BasicVisualElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {

        }

        [ShowInInspector]
        protected ITooltipProvider tooltipProvider { get; private set; }

        protected UIPanelController source;

        public event Action OnMouseEnter;
        public event Action OnMouseLeave;

        public BasicVisualElement() : base()
        {
            RegisterCallback<MouseEnterEvent>(e =>
            {
                if (tooltipProvider != null)
                {
                    TooltipManager.Open(tooltipProvider, null);
                }

                OnMouseEnter?.Invoke();
            });

            RegisterCallback<MouseLeaveEvent>(e =>
            {
                if (tooltipProvider != null)
                {
                    TooltipManager.Close(tooltipProvider);
                }

                OnMouseLeave?.Invoke();
            });
        }

        public void SetTooltip(ITooltipProvider provider)
        {
            tooltipProvider = provider;
        }

        public void SetTooltip(LocalizedStringReference newTooltip)
        {
            tooltipProvider = new TempTooltipProvider(newTooltip);
        }

        public void SetSource(UIPanelController source)
        {
            //if (this.source != null)
            //{
            //    Debug.LogWarning($"This {GetType()} has already been set source.");
            //}

            this.source = source;
        }
    }
}
