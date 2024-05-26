using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;

namespace VMFramework.UI
{
    public interface IUIToolkitTracingUIPanel : ITracingUIPanel, IUIToolkitUIPanelController
    {
        protected Vector2 referenceResolution { get; }

        protected bool enableOverflow { get; }

        protected bool autoPivotCorrection { get; }

        protected Vector2 defaultPivot { get; }

        protected bool useRightPosition { get; }

        protected bool useTopPosition { get; }

        protected VisualElement tracingContainer { get; }
        
        bool ITracingUIPanel.TryUpdatePosition(Vector2 screenPosition)
        {
            Vector2 screenSize = new(Screen.width, Screen.height);
            var boundsSize = referenceResolution;

            var position = screenPosition.Divide(screenSize).Multiply(boundsSize);

            var width = tracingContainer.resolvedStyle.width;
            var height = tracingContainer.resolvedStyle.height;

            if (width <= 0 || height <= 0 || width.IsNaN() || height.IsNaN())
            {
                return false;
            }

            if (enableOverflow == false)
            {
                position = position.Clamp(boundsSize);

                if (autoPivotCorrection)
                {
                    var pivot = defaultPivot;

                    if (position.x < defaultPivot.x * width)
                    {
                        pivot.x = (position.x / width).ClampMin(0);
                    }
                    else if (position.x > boundsSize.x - (1 - defaultPivot.x) * width)
                    {
                        pivot.x = (1 - (boundsSize.x - position.x) / width).ClampMax(1);
                    }

                    if (position.y < defaultPivot.y * height)
                    {
                        pivot.y = (position.y / height).ClampMin(0);
                    }
                    else if (position.y > boundsSize.y - (1 - defaultPivot.y) * height)
                    {
                        pivot.y = (1 - (boundsSize.y - position.y) / height).ClampMax(1);
                    }

                    SetPivot(pivot);
                }
            }

            tracingContainer.SetPosition(position, useRightPosition, useTopPosition, boundsSize);
            
            return true;
        }

        void ITracingUIPanel.SetPivot(Vector2 pivot)
        {
            tracingContainer.style.translate = new StyleTranslate(new Translate(
                new Length(-100 * pivot.x, LengthUnit.Percent),
                new Length(100 * pivot.y, LengthUnit.Percent)));
        }
    }
}