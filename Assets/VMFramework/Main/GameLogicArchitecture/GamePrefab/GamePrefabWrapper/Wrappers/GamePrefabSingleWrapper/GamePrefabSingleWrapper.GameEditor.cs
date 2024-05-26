#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabSingleWrapper : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.icon
        {
            get
            {
                if (gamePrefab is IGameEditorMenuTreeNode node)
                {
                    return node.icon;
                }
                
                return Icon.None;
            }
        }

        protected override IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            if (gamePrefab is IGameEditorToolBarProvider provider)
            {
                foreach (var config in provider.GetToolbarButtons())
                {
                    yield return config;
                }
            }
            
            yield return new(EditorNames.openGamePrefabScriptButtonName, OpenScriptOfGamePrefab);

            yield return new(EditorNames.openGameItemScriptButtonName, OpenScriptOfGameItem);
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }
        }

        private void OpenScriptOfGamePrefab()
        {
            gamePrefab?.OpenScriptOfObject();
        }

        private void OpenScriptOfGameItem()
        {
            gamePrefab?.gameItemType?.OpenScriptOfType();
        }
    }
}
#endif