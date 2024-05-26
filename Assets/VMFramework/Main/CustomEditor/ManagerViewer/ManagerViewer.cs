#if UNITY_EDITOR
using UnityEditor;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    internal sealed class ManagerViewer : SimpleOdinEditorWindow<ManagerViewerContainer>
    {
        public const string EDITOR_NAME = "Manager Viewer";

        [MenuItem("Tools/" + EDITOR_NAME)]
        public static void OpenWindow() => GetSimpleWindow<ManagerViewer>(EDITOR_NAME);
    }
}
#endif