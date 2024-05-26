#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using VMFramework.Core.Editor;

namespace VMFramework.Editor.GameEditor
{
    [Serializable]
    internal class AuxiliaryTools
    {
        [PropertySpace(SpaceBefore = 50)]
        [Button("打开EditorIcons概览", ButtonSizes.Large)]
        private void OpenEditorIconsOverview()
        {
            EditorIconsOverview.OpenEditorIconsOverview();
        }

        [Button("重绘GUI", ButtonSizes.Large)]
        private void RequestRepaint()
        {
            GUIHelper.RequestRepaint();
        }

        [Button("打开游戏编辑器脚本", ButtonSizes.Large)]
        private void OpenGameEditorScript()
        {
            typeof(GameEditor).OpenScriptOfType();
        }
    }
}
#endif