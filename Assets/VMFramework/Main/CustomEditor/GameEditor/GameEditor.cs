#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor.GameEditor
{
    internal class GameEditor : OdinMenuEditorWindow
    {
        private readonly AuxiliaryTools auxiliaryTools = new();

        [MenuItem("Tools/" + GameEditorNames.GAME_EDITOR_DEFAULT_NAME + " #G")]
        [Shortcut("Open Game Editor", KeyCode.G, ShortcutModifiers.Shift)]
        private static void OpenWindow()
        {
            GameCoreSettingFile.CheckGlobal();

            var editorName = GameEditorNames.gameEditorName;
            var window = CreateWindow<GameEditor>(editorName);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            if (GameCoreSetting.gameCoreSettingsFile == null)
            {
                return new OdinMenuTree(true);
            }

            GameCoreSetting.gameCoreSettingsFile.AutoFindSetting();

            var gameEditorSetting = GameCoreSetting.gameEditorGeneralSetting;

            var auxiliaryToolsCategoryName = GameEditorNames.auxiliaryToolsCategoryName;
            var generalSettingsCategoryName = GameEditorNames.generalSettingsCategoryName;

            OdinMenuTree tree = new(true)
            {
                { auxiliaryToolsCategoryName, auxiliaryTools, EditorIcons.HamburgerMenu },
                {
                    generalSettingsCategoryName, GameCoreSetting.gameCoreSettingsFile,
                    SdfIconType.GearFill
                },
                {
                    $"{generalSettingsCategoryName}/{GameEditorNames.editorCategoryName}", null,
                    EditorIcons.UnityLogo
                },
                {
                    $"{generalSettingsCategoryName}/{GameEditorNames.coreCategoryName}", null,
                    EditorIcons.StarPointer
                },
                {
                    $"{generalSettingsCategoryName}/{GameEditorNames.resourcesManagementCategoryName}", null,
                    SdfIconType.Boxes
                },
                {
                    $"{generalSettingsCategoryName}/{GameEditorNames.builtInCategoryName}", null,
                    SdfIconType.Inboxes
                },
                { "具体设置", null, SdfIconType.GearFill }
            };

            tree.DefaultMenuStyle.IconSize = 24.00f;
            tree.Config.DrawSearchToolbar = true;

            var generalSettingsPathDict = new Dictionary<GeneralSetting, string>();

            foreach (var generalSetting in GameCoreSetting.GetAllGeneralSettings())
            {
                if (generalSetting is not IGameEditorMenuTreeNode generalSettingNode)
                {
                    continue;
                }

                var folderPath = generalSettingNode.folderPath;
                folderPath = folderPath.Replace("\\", "/");
                folderPath = folderPath.Trim('/');

                var totalPath = generalSettingsCategoryName;

                if (folderPath.IsNullOrEmptyAfterTrim() == false)
                {
                    totalPath += $"/{folderPath}";
                }

                totalPath += $"/{generalSettingNode.name}";
                
                tree.Add(totalPath, generalSetting, generalSettingNode.icon);

                generalSettingsPathDict.Add(generalSetting, totalPath);
            }

            foreach (var (generalSetting, totalPath) in generalSettingsPathDict)
            {
                if (generalSetting is IGameEditorMenuTreeNodesProvider
                    {
                        isMenuTreeNodesVisible: true
                    } menuTreeNodeProvider)
                {
                    var path = totalPath;

                    var allNodes = menuTreeNodeProvider.GetAllMenuTreeNodes()?.ToList();
                    
                    if (allNodes == null)
                    {
                        Debug.LogWarning($"{menuTreeNodeProvider}获取的节点列表为Null");
                        continue;
                    }

                    if (menuTreeNodeProvider.autoStackMenuTreeNodes)
                    {
                        if (gameEditorSetting == null || allNodes.Count >
                            gameEditorSetting.autoStackMenuTreeNodesMaxCount)
                        {
                            path += "/具体设置";
                        }
                    }

                    foreach (var menuTreeNode in allNodes)
                    {
                        if (menuTreeNode == null)
                        {
                            continue;
                        }

                        tree.Add($"{path}/{menuTreeNode.name}", menuTreeNode, menuTreeNode.icon);
                    }
                }
            }

            tree.EnumerateTree().ForEach(AddRightClickContextMenu);

            return tree;
        }

        private void AddRightClickContextMenu(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += menuItem =>
            {
                if (menuItem.Value is not IGameEditorContextMenuProvider contextMenuProvider )
                {
                    return;
                }
                
                if (Event.current.type == EventType.MouseDown && Event.current.button == 1 &&
                    menuItem.Rect.Contains(Event.current.mousePosition))
                {
                    GenericMenu menu = new GenericMenu();

                    foreach (var config in contextMenuProvider.GetMenuItems())
                    {
                        menu.AddItem(new GUIContent(config.name, config.tooltip), false,
                            () => config.onClick?.Invoke());
                    }

                    menu.ShowAsContext();

                    Event.current.Use();
                }
            };
        }

        protected override void OnBeginDrawEditors()
        {
            if (MenuTree?.Selection == null)
            {
                return;
            }

            var selected = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected is { Value: not null })
                {
                    GUILayout.Label(selected.Name);

                    if (selected.Value is IGameEditorToolBarProvider toolBarProvider)
                    {
                        foreach (var buttonConfig in toolBarProvider.GetToolbarButtons())
                        {
                            if (SirenixEditorGUI.ToolbarButton(new GUIContent(buttonConfig.name,
                                    buttonConfig.tooltip)))
                            {
                                buttonConfig.onClick?.Invoke();
                            }
                        }
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}
#endif