#if UNITY_EDITOR
using UnityEngine;

namespace VMFramework.Editor.ProjectItemContextMenu
{
    public interface IButtonDrawer
    {
        public bool CanDrawButton(ProjectItem item);

        public bool Button(Rect rect);
    }
}
#endif