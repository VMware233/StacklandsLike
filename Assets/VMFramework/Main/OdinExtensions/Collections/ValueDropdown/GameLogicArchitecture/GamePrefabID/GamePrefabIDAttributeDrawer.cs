#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public abstract class GamePrefabIDAttributeDrawer<TAttribute> : GeneralValueDropdownAttributeDrawer<TAttribute>
        where TAttribute : GamePrefabIDAttribute
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            if (Attribute.FilterByGameItemType)
            {
                return GamePrefabManager.GetGamePrefabNameListByGameItemType(Attribute.GameItemType);
            }
            return GamePrefabManager.GetGamePrefabNameListByType(Attribute.GamePrefabTypes);
        }

        protected override Texture GetSelectorIcon(object value)
        {
            if (value is not string id)
            {
                return null;
            }

            if (GamePrefabManager.TryGetGamePrefab(id, out var prefab) == false)
            {
                return null;
            }

            if (prefab is not IGameEditorMenuTreeNode node)
            {
                return null;
            }

            return node.icon.GetTexture();
        }

        protected override void DrawCustomButtons()
        {
            base.DrawCustomButtons();

            if (Property.ValueEntry.WeakSmartValue is not string id)
            {
                return;
            }

            if (GamePrefabWrapperQuery.TryGetGamePrefabWrapper(id, out var wrapper) == false)
            {
                return;
            }

            if (Button("打开编辑窗口", SdfIconType.PencilSquare))
            {
                GUIHelper.OpenInspectorWindow(wrapper);
            }
        }
    }

    public class GamePrefabIDAttributeDrawer : GamePrefabIDAttributeDrawer<GamePrefabIDAttribute>
    {
        
    }
}
#endif