// using System.Collections;
// using System.Collections.Generic;
// using VMFramework.UI;
// using VMFramework.Configuration;
// using VMFramework.GameLogicArchitecture;
// using Newtonsoft.Json;
// using Sirenix.OdinInspector;
// using VMFramework.Core;
// using VMFramework.Localization;
// using VMFramework.OdinExtensions;
//
// public class VisualGamePrefabBundle<TPrefab, TGeneralSetting> :
//     GamePrefabCoreBundle<TPrefab, TGeneralSetting>
//     where TPrefab : VisualGamePrefabBundle<TPrefab, TGeneralSetting>.GameItemPrefab
//     where TGeneralSetting : VisualGamePrefabBundle<TPrefab, TGeneralSetting>.
//     GameItemGeneralSetting
// {
//     public new class GameItemPrefab :
//         GamePrefabCoreBundle<TPrefab, TGeneralSetting>.GameItemPrefab,
//         ITracingTooltipProvider
//     {
//         [LabelText("名称格式覆盖", SdfIconType.Bootstrap),
//          TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//         [JsonProperty]
//         public TextTagFormat nameFormat = new();
//
//         [LabelText("是否有描述"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//         [JsonProperty]
//         public bool hasDescription = false;
//
//         [LabelText("描述", SdfIconType.BlockquoteLeft),
//          TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//         [Indent]
//         [ShowIf(nameof(hasDescription))]
//         [JsonProperty]
//         public LocalizedStringReference description = new();
//
//         [LabelText("描述格式覆盖", SdfIconType.Bootstrap),
//          TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//         [Indent]
//         [ShowIf(nameof(hasDescription))]
//         [JsonProperty]
//         public TextTagFormat descriptionFormat = new();
//
//         #region GUI
//
//         protected override void OnInspectorInit()
//         {
//             base.OnInspectorInit();
//
//             nameFormat ??= new();
//             description ??= new();
//             descriptionFormat ??= new();
//         }
//
//         #endregion
//
//         #region Localization
//
//         public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
//         {
//             base.AutoConfigureLocalizedString(settings);
//
//             description ??= new();
//             
//             description.AutoConfig("", id.ToPascalCase() + "Description", settings.defaultTableName);
//         }
//
//         public override void CreateLocalizedStringKeys()
//         {
//             base.CreateLocalizedStringKeys();
//
//             if (hasDescription)
//             {
//                 description.CreateNewKey();
//             }
//         }
//
//         #endregion
//
//         #region Tooltip
//
//         string ITracingTooltipProvider.GetTooltipID()
//         {
//             return GetStaticGeneralSetting().tooltipID;
//         }
//
//         int ITracingTooltipProvider.GetTooltipPriority()
//         {
//             return GetStaticGeneralSetting().tooltipPriorityConfig;
//         }
//
//         public virtual string GetTooltipTitle()
//         {
//             return nameFormat.GetText(name);
//         }
//
//         public virtual IEnumerable<ITracingTooltipProvider.PropertyConfig> GetTooltipProperties()
//         {
//             yield break;
//         }
//
//         public virtual string GetTooltipDescription()
//         {
//             if (hasDescription)
//             {
//                 return descriptionFormat.GetText(description);
//             }
//
//             return string.Empty;
//         }
//
//         #endregion
//     }
//
//     public new class GameItemGeneralSetting :
//         GamePrefabCoreBundle<TPrefab, TGeneralSetting>.GameItemGeneralSetting
//     {
//         public const string TOOLTIP_SETTING_CATEGORY = "提示框设置";
//
//         [LabelText("提示框"),
//          TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY,
//              SdfIconType.ColumnsGap, TextColor = "purple")]
//         [ValueDropdown(nameof(GetTooltipIDList))]
//         [IsNotNullOrEmpty]
//         [JsonProperty]
//         public string tooltipID;
//
//         [LabelText("提示框优先级"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
//         [JsonProperty]
//         public TooltipPriorityConfig tooltipPriorityConfig = new();
//
//         #region GUI
//
//         private IEnumerable GetTooltipIDList()
//         {
//             return GamePrefabManager.GetGamePrefabNameListByType(typeof(UIToolkitTracingTooltipPreset));
//         }
//
//         #endregion
//     }
// }
