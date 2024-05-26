using Sirenix.OdinInspector;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class CarouselGroupVisualElement : VisualElement
    {
        #region Traits

        [Preserve]
        public new class UxmlFactory : UxmlFactory<CarouselGroupVisualElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {

        }

        #endregion

        public override VisualElement contentContainer => container ?? this;

        [ShowInInspector]
        public VisualElement container { get; }

        public CarouselGroupVisualElement() : base()
        {
            var container = new VisualElement()
            {
                name = "Container"
            };
            container.AddToClassList("carouselGroupContainer");

            Add(container);

            this.container = container;
        }
    }
}