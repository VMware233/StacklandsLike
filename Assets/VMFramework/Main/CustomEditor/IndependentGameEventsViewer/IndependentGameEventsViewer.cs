#if UNITY_EDITOR
using UnityEditor;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor.IndependentGameEventsViewer
{
    internal sealed class IndependentGameEventsViewer
        : SimpleOdinEditorWindow<IndependentGameEventsViewerContainer>
    {
        public const string EDITOR_NAME = "Independent Game Events Viewer";

        [MenuItem("Tools/" + EDITOR_NAME)]
        public static void OpenWindow()
        {
            GetSimpleWindow<IndependentGameEventsViewer>(EDITOR_NAME);
        }
    }
}
#endif