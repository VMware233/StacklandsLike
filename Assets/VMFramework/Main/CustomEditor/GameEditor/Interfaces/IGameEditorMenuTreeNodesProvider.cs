#if UNITY_EDITOR
using System.Collections.Generic;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorMenuTreeNodesProvider
    {
        public bool autoStackMenuTreeNodes => false;

        public bool isMenuTreeNodesVisible { get; }

        public IEnumerable<IGameEditorMenuTreeNode> GetAllMenuTreeNodes();
    }
}
#endif