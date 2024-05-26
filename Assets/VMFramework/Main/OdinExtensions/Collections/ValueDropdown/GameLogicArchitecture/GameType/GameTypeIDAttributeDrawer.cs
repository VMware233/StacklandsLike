#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GameTypeIDAttributeDrawer : GeneralValueDropdownAttributeDrawer<GameTypeIDAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            if (Attribute.LeafGameTypesOnly == false)
            {
                return GameTypeNameUtility.GetAllGameTypeNameList();
            }
            
            return GameTypeNameUtility.GetGameTypeNameList();
        }

        protected override void DrawCustomButtons()
        {
            base.DrawCustomButtons();

            if (Button("跳转到游戏类型编辑器", SdfIconType.Search))
            {
                var gameEditor = EditorWindow.GetWindow<GameEditor>();
                
                var menuTree = gameEditor.MenuTree;
                
                foreach (var menuItem in menuTree.EnumerateTree())
                {
                    if (menuItem.Value is GameTypeGeneralSetting)
                    {
                        if (menuTree.Selection.Contains(menuItem))
                        {
                            menuTree.Selection.Remove(menuItem);
                        }
                        else
                        {
                            menuTree.Selection.Add(menuItem);
                        }
                    }
                }
            }
        }
    }
}
#endif