using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.UI
{
    public interface IUGUITracingUIPanel : ITracingUIPanel, IUGUIPanelController
    {
        protected Vector2 referenceResolution { get; }

        protected bool enableOverflow { get; }

        protected bool autoPivotCorrection { get; }

        protected Vector2 defaultPivot { get; }

        protected RectTransform tracingContainer { get; }

        [Button("更新位置")]
        bool ITracingUIPanel.TryUpdatePosition(Vector2 screenPosition)
        {
            Vector2 screenSize = new(Screen.width, Screen.height);
            var boundsSize = referenceResolution;

            var position = screenPosition.Divide(screenSize).Multiply(boundsSize);

            var width = tracingContainer.GetWidth();
            var height = tracingContainer.GetHeight();

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

            tracingContainer.anchoredPosition = position;

            return true;
        }

        void ITracingUIPanel.SetPivot(Vector2 pivot)
        {
            tracingContainer.pivot = pivot;
        }
    }
}