#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorMenuTreeNode : INameOwner
    {
        public string folderPath => "";

        public Icon icon => Icon.None;
        
        public SdfIconType sdfIcon => SdfIconType.None;

        public string nodePath
        {
            get
            {
                if (folderPath.IsNullOrEmpty())
                {
                    return name;
                }

                return folderPath + "/" + name;
            }
        }
    }
}
#endif