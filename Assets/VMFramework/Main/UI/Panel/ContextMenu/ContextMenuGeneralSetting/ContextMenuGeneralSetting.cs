using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed partial class ContextMenuGeneralSetting : GeneralSetting
    {
        private const string CONTEXT_MENU_CATEGORY = "Context Menu";

        private const string CONTEXT_MENU_ID_BIND_CATEGORY =
            TAB_GROUP_NAME + "/" + CONTEXT_MENU_CATEGORY + "/Context Menu ID Bind";

        [PropertyTooltip("默认上下文菜单")]
        [TabGroup(TAB_GROUP_NAME, CONTEXT_MENU_CATEGORY), TitleGroup(CONTEXT_MENU_ID_BIND_CATEGORY)]
        [GamePrefabID(typeof(IContextMenuPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty, SerializeField]
        public string defaultContextMenuID;
        
        [PropertyTooltip("上下文菜单ID绑定配置")]
        [TitleGroup(CONTEXT_MENU_ID_BIND_CATEGORY)]
        [JsonProperty]
        public GameTypeBasedConfigs<ContextMenuBindConfig> contextMenuIDBindConfigs = new();

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (defaultContextMenuID.IsNullOrEmpty())
            {
                Debug.LogWarning($"{nameof(defaultContextMenuID)} is not set.");
            }
            
            contextMenuIDBindConfigs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            contextMenuIDBindConfigs.Init();
        }
    }
}
