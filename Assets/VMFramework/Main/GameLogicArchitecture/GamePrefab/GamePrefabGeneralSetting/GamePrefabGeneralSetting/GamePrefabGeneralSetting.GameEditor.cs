#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting : IGameEditorMenuTreeNodesProvider, IGameEditorMenuTreeNode
    {
        bool IGameEditorMenuTreeNodesProvider.isMenuTreeNodesVisible => true;

        IEnumerable<IGameEditorMenuTreeNode> IGameEditorMenuTreeNodesProvider.GetAllMenuTreeNodes()
        {
            return initialGamePrefabWrappers;
        }

        #region Toolbar

        protected override IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.openGamePrefabScriptButtonName, OpenGamePrefabScript);
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }

            yield return new(EditorNames.saveAllButtonName, SaveAllGamePrefabs);
        }

        private void OpenGamePrefabScript()
        {
            baseGamePrefabType.OpenScriptOfType();
        }

        private void SaveAllGamePrefabs()
        {
            this.EnforceSave();
            
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                wrapper.SetEditorDirty();
            }
        }

        #endregion

        #region Icon

        private IGameEditorMenuTreeNode iconGamePrefab;

        Icon IGameEditorMenuTreeNode.icon
        {
            get
            {
                foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs(baseGamePrefabType))
                {
                    if (gamePrefab is not IGameEditorMenuTreeNode node)
                    {
                        continue;
                    }

                    if (node.icon.IsNull() == false)
                    {
                        iconGamePrefab = node;
                        return node.icon;
                    }
                }
                
                return Icon.None;
            }
        }

        SdfIconType IGameEditorMenuTreeNode.sdfIcon => iconGamePrefab?.sdfIcon ?? SdfIconType.None;

        #endregion
    }
}
#endif