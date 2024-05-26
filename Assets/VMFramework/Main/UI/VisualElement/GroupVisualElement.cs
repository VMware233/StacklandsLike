using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class GroupVisualElement : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<GroupVisualElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {

        }

        public GroupVisualElement() : base()
        {
            AddToClassList("group");
        }
    }
}
