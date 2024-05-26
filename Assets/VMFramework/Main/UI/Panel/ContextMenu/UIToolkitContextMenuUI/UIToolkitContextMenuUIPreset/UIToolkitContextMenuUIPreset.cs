using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class UIToolkitContextMenuUIPreset : UIToolkitTracingUIPanelPreset, IContextMenuPreset
    {
        public const string CONTEXT_MENU_UI_CATEGORY = "上下文菜单界面设置";
        
        public const string ENTRY_SELECTED_ICON_ASSET_NAME = "Entry Selecting";

        public override Type controllerType =>
            typeof(UIToolkitContextMenuUIController);

        [LabelText("上下文菜单条目容器名称"), TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [VisualElementName]
        [JsonProperty]
        public string contextMenuEntryContainerName;

        [LabelText("条目被选中的图标"), TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        public Sprite entrySelectedIcon;

        [LabelText("自动执行上下文菜单条目"), SuffixLabel("如果只有一条"),
         TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [JsonProperty]
        public bool autoExecuteIfOnlyOneEntry = true;

        [LabelText("点击的鼠标按键类型"), TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [JsonProperty]
        public MouseButtonType clickMouseButtonType = MouseButtonType.LeftButton;

        [LabelText("全局事件触发时关闭此UI"), TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_UI_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [JsonProperty]
        public List<string> gameEventIDsToClose = new();

        public override void CheckSettings()
        {
            base.CheckSettings();

            isUnique = true;
        }
    }
}
