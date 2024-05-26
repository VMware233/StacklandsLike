// using Sirenix.OdinInspector;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using VMFramework.UI;
// using VMFramework.Configuration;
// using Basis.Core;
// using VMFramework.Core;
// using UnityEngine;
// using Newtonsoft.Json;
// using VMFramework.Core.Linq;
// using VMFramework.GlobalEvent;
// using VMFramework.Localization;
// using VMFramework.OdinExtensions;
// using VMFramework.Property;
//
// namespace VMFramework.GameLogicArchitecture
// {
//     public class VisualGameItemBundle<TPrefab, TGeneralSetting, TInstance> :
//             SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance>
//         where TPrefab : VisualGameItemBundle<TPrefab, TGeneralSetting, TInstance>.
//         GameItemPrefab, new()
//         where TInstance : VisualGameItemBundle<TPrefab, TGeneralSetting, TInstance>.
//         GameItem
//         where TGeneralSetting : VisualGameItemBundle<TPrefab, TGeneralSetting,
//             TInstance>.
//         GameItemGeneralSetting
//
//     {
//         private static Dictionary<Type, List<PropertyConfig>> propertyConfigsDict =
//             new();
//
//         public new abstract class GameItem : 
//             SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance>.GameItem,
//             ITracingTooltipProvider
//         {
//             #region Tooltip
//
//             bool ITracingTooltipProvider.TryGetTooltipBindGlobalEvent(
//                 out GlobalEventConfig globalEvent)
//             {
//                 globalEvent = generalSetting.tooltipBindGlobalEvent;
//
//                 return globalEvent != null;
//             }
//
//             string ITracingTooltipProvider.GetTooltipID()
//             {
//                 return generalSetting.tooltipID;
//             }
//
//             int ITracingTooltipProvider.GetTooltipPriority()
//             {
//                 return generalSetting.tooltipPriorityConfig;
//             }
//
//             public virtual string GetTooltipTitle()
//             {
//                 return origin.nameFormat.GetText(name);
//             }
//
//             public virtual IEnumerable<ITracingTooltipProvider.PropertyConfig> GetTooltipProperties()
//             {
//                 foreach (var tooltipPropertyConfig in origin.GetTooltipPropertyConfigs())
//                 {
//                     string AttributeValueGetter() =>
//                         $"{tooltipPropertyConfig.propertyConfig.name}:" +
//                         $"{tooltipPropertyConfig.propertyConfig.GetValueString(this)}";
//
//                     yield return new ITracingTooltipProvider.PropertyConfig()
//                     {
//                         attributeValueGetter = AttributeValueGetter,
//                         isStatic = tooltipPropertyConfig.isStatic,
//                         icon = tooltipPropertyConfig.propertyConfig.icon
//                     };
//                 }
//             }
//
//             public virtual string GetTooltipDescription()
//             {
//                 if (origin.hasDescription)
//                 {
//                     return origin.descriptionFormat.GetText(origin.description);
//                 }
//
//                 return string.Empty;
//             }
//
//             #endregion
//
//             #region Property
//
//             [ShowInInspector]
//             private Dictionary<string, PropertyOfGameItem> properties;
//
//             private void GenerateProperties()
//             {
//                 properties = new();
//
//                 var propertyConfigs = GetPropertyConfigs();
//
//                 foreach (var propertyConfig in propertyConfigs)
//                 {
//                     var property = PropertyOfGameItem.Create(propertyConfig.id, this);
//
//                     properties.Add(property.id, property);
//                 }
//             }
//
//             [Button("获得属性")]
//             public IEnumerable<PropertyOfGameItem> GetAllProperties()
//             {
//                 if (properties == null)
//                 {
//                     GenerateProperties();
//                 }
//
//                 return properties.Values;
//             }
//
//             public PropertyOfGameItem GetProperty(string propertyID)
//             {
//                 if (properties == null)
//                 {
//                     GenerateProperties();
//                 }
//
//                 return properties.GetValueOrDefault(propertyID);
//             }
//
//             public PropertyOfGameItem GetPropertyStrictly(string propertyID)
//             {
//                 var property = GetProperty(propertyID);
//
//                 if (property == null)
//                 {
//                     Note.note.Error($"无法获取属性{propertyID}");
//                 }
//
//                 return property;
//             }
//
//             private IReadOnlyList<PropertyConfig> GetPropertyConfigs()
//             {
//                 if (propertyConfigsDict.TryGetValue(GetType(),
//                         out var propertyConfigs))
//                 {
//                     return propertyConfigs;
//                 }
//
//                 propertyConfigs = PropertyGeneralSetting.GetPropertyConfigs(GetType());
//
//                 propertyConfigsDict[GetType()] = propertyConfigs;
//
//                 return propertyConfigs;
//             }
//
//             #endregion
//         }
//
//         public new abstract class GameItemPrefab :
//             SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance>.GameItemPrefab
//         {
//             [LabelText("名称格式覆盖", SdfIconType.Bootstrap),
//              TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [JsonProperty]
//             public TextTagFormat nameFormat = new();
//
//             [LabelText("是否有描述"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [JsonProperty]
//             public bool hasDescription = false;
//
//             [LabelText("描述", SdfIconType.BlockquoteLeft),
//              TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [Indent]
//             [ShowIf(nameof(hasDescription))]
//             [JsonProperty]
//             public LocalizedStringReference description = new();
//
//             [LabelText("描述格式覆盖", SdfIconType.Bootstrap),
//              TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [Indent]
//             [ShowIf(nameof(hasDescription))]
//             [JsonProperty]
//             public TextTagFormat descriptionFormat = new();
//
//             [LabelText("自定义提示框显示的属性"),
//              TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [SerializeField, JsonProperty]
//             private bool customTooltipProperties = false;
//
//             [LabelText("自定义模式"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [Indent]
//             [ShowIf(nameof(customTooltipProperties))]
//             [SerializeField, JsonProperty]
//             private CustomTooltipPropertiesMode customTooltipPropertiesMode;
//
//             [LabelText("提示框显示的属性"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [Indent]
//             [ShowIf(nameof(customTooltipProperties))]
//             [ListDrawerSettings(CustomAddFunction = nameof(AddTooltipPropertyConfigsGUI))]
//             [SerializeField, JsonProperty]
//             private List<TooltipPropertyConfig> tooltipPropertyConfigs = new();
//
//             [LabelText("提示框属性运行时"), TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
//             [ShowInInspector, HideInEditorMode]
//             private List<TooltipPropertyConfigRuntime> tooltipPropertyConfigsRuntime;
//
//             #region GUI
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 nameFormat ??= new();
//                 description ??= new();
//                 descriptionFormat ??= new();
//
//                 tooltipPropertyConfigs ??= new();
//
//                 foreach (var tooltipPropertyConfig in tooltipPropertyConfigs)
//                 {
//                     tooltipPropertyConfig.filterType = actualInstanceType;
//                 }
//             }
//
//             private TooltipPropertyConfig AddTooltipPropertyConfigsGUI()
//             {
//                 return new TooltipPropertyConfig()
//                 {
//                     filterType = actualInstanceType
//                 };
//             }
//
//             #endregion
//
//             #region Localization
//
//             public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
//             {
//                 base.AutoConfigureLocalizedString(settings);
//
//                 description ??= new();
//
//                 description.defaultValue ??= "";
//                 
//                 if (description.key.IsNullOrEmptyAfterTrim())
//                 {
//                     description.key = id.ToPascalCase() + "Description";
//                 }
//                 
//                 if (description.tableName.IsNullOrEmptyAfterTrim() &&
//                     settings.defaultTableName.IsNullOrEmptyAfterTrim() == false)
//                 {
//                     description.tableName = settings.defaultTableName;
//                 }
//             }
//
//             public override void CreateLocalizedStringKeys()
//             {
//                 base.CreateLocalizedStringKeys();
//
//                 if (hasDescription)
//                 {
//                     description.CreateNewKey();
//                 }
//             }
//
//             #endregion
//
//             public override void CheckSettings()
//             {
//                 base.CheckSettings();
//
//                 if (customTooltipProperties)
//                 {
//                     foreach (var tooltipPropertyConfig in tooltipPropertyConfigs)
//                     {
//                         Note.note.AssertIsNotNullOrEmpty(
//                             tooltipPropertyConfig.propertyID,
//                             $"{nameof(tooltipPropertyConfig)}." +
//                             $"{nameof(tooltipPropertyConfig.propertyID)}");
//                     }
//
//                     if (tooltipPropertyConfigs.ContainsSame(config =>
//                             config.propertyID))
//                     {
//                         Debug.LogWarning($"{id}的提示框属性设置有重复的属性ID");
//                     }
//                 }
//             }
//
//             protected override void OnInit()
//             {
//                 base.OnInit();
//
//                 foreach (var tooltipPropertyConfig in tooltipPropertyConfigs)
//                 {
//                     tooltipPropertyConfig.Init();
//                 }
//
//                 tooltipPropertyConfigsRuntime = new();
//
//                 if (customTooltipProperties)
//                 {
//                     tooltipPropertyConfigsRuntime = new();
//
//                     if (customTooltipPropertiesMode ==
//                         CustomTooltipPropertiesMode.Incremental)
//                     {
//                         foreach (var config in GetDefaultTooltipPropertyConfigs())
//                         {
//                             tooltipPropertyConfigsRuntime.Add(config);
//                         }
//                     }
//
//                     foreach (var config in tooltipPropertyConfigs)
//                     {
//                         tooltipPropertyConfigsRuntime.Add(config.ConvertToRuntime());
//                     }
//                 }
//                 else
//                 {
//                     foreach (var config in GetDefaultTooltipPropertyConfigs())
//                     {
//                         tooltipPropertyConfigsRuntime.Add(config);
//                     }
//                 }
//             }
//
//             public IEnumerable<TooltipPropertyConfigRuntime>
//                 GetDefaultTooltipPropertyConfigs()
//             {
//                 return GetStaticGeneralSettingStrictly()
//                     .GetDefaultTooltipPropertyConfigsConfigs(actualInstanceType);
//             }
//
//             public IReadOnlyList<TooltipPropertyConfigRuntime>
//                 GetTooltipPropertyConfigs()
//             {
//                 return tooltipPropertyConfigsRuntime;
//             }
//         }
//
//         public new abstract class GameItemGeneralSetting :
//             SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance>.
//             GameItemGeneralSetting
//         {
//             public const string TOOLTIP_SETTING_CATEGORY = "提示框设置";
//
//             public const string PROPERTY_SETTING_CATEGORY = "属性设置";
//
//             [LabelText("提示框"),
//              TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY,
//                  SdfIconType.ColumnsGap, TextColor = "purple")]
//             [ValueDropdown(nameof(GetTooltipIDList))]
//             [IsNotNullOrEmpty]
//             [JsonProperty]
//             public string tooltipID;
//
//             [LabelText("提示框优先级"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
//             [JsonProperty]
//             public TooltipPriorityConfig tooltipPriorityConfig = new();
//
//             [LabelText("提示框绑定的全局事件"),
//              TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
//             [ValueDropdown(nameof(GetGlobalEventNameList))]
//             [JsonProperty, SerializeField]
//             private string tooltipBindGlobalEventID;
//
//             public GlobalEventConfig tooltipBindGlobalEvent { get; private set; }
//
//             [LabelText("默认提示框显示的属性"),
//              TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
//             [ListDrawerSettings(CustomAddFunction =
//                 nameof(AddInstanceTooltipPropertyConfigGUI))]
//             [SerializeField, JsonProperty]
//             private List<InstanceTooltipPropertyConfig>
//                 defaultInstanceTooltipPropertyConfigs = new();
//
//             [TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
//             [ShowInInspector]
//             [HideInEditorMode]
//             private Dictionary<Type, InstanceTooltipPropertyConfig>
//                 defaultInstanceTooltipPropertyConfigsRuntime;
//
//             [LabelText("所有类对应的属性设置"),
//              TabGroup(TAB_GROUP_NAME, PROPERTY_SETTING_CATEGORY)]
//             [ShowInInspector]
//             private static Dictionary<Type, List<PropertyConfig>>
//                 propertyConfigsDict =>
//                 VisualGameItemBundle<TPrefab, TGeneralSetting, TInstance>
//                     .propertyConfigsDict;
//
//             #region GUI
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 tooltipPriorityConfig ??= new();
//             }
//
//             private InstanceTooltipPropertyConfig
//                 AddInstanceTooltipPropertyConfigGUI()
//             {
//                 return new();
//             }
//
//             private IEnumerable GetTooltipIDList()
//             {
//                 return GamePrefabManager.GetGamePrefabNameListByType(
//                     typeof(UIToolkitTracingTooltipPreset));
//             }
//
//             private IEnumerable GetGlobalEventNameList()
//             {
//                 return GamePrefabManager.GetGamePrefabNameListByType(typeof(GlobalEventConfig));
//             }
//
//             #endregion
//
//             #region Init
//
//             protected override void OnPreInit()
//             {
//                 base.OnPreInit();
//
//                 defaultInstanceTooltipPropertyConfigsRuntime = new();
//
//                 foreach (var instanceTooltipPropertyConfig in
//                          defaultInstanceTooltipPropertyConfigs)
//                 {
//                     instanceTooltipPropertyConfig.Init();
//
//                     defaultInstanceTooltipPropertyConfigsRuntime[
//                             instanceTooltipPropertyConfig.instanceType] =
//                         instanceTooltipPropertyConfig;
//                 }
//             }
//
//             protected override void OnPostInit()
//             {
//                 base.OnPostInit();
//
//                 if (tooltipBindGlobalEventID.IsNullOrEmpty() == false)
//                 {
//                     tooltipBindGlobalEvent =
//                         GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(tooltipBindGlobalEventID);
//                 }
//             }
//
//             #endregion
//
//             public IEnumerable<TooltipPropertyConfigRuntime>
//                 GetDefaultTooltipPropertyConfigsConfigs(Type currentInstanceType)
//             {
//                 var result = new List<TooltipPropertyConfigRuntime>();
//
//                 foreach (var (instanceType, instanceTooltipProperty) in
//                          defaultInstanceTooltipPropertyConfigsRuntime)
//                 {
//                     if (currentInstanceType.IsDerivedFrom(instanceType, true))
//                     {
//                         foreach (var config in instanceTooltipProperty
//                                      .GetTooltipPropertyConfigs())
//                         {
//                             result.Add(config);
//                         }
//                     }
//                 }
//
//                 return result.Distinct(config => config.propertyConfig);
//             }
//         }
//
//         #region Tooltip
//
//         public enum CustomTooltipPropertiesMode
//         {
//             [LabelText("覆盖")]
//             Override,
//
//             [LabelText("增量")]
//             Incremental
//         }
//
//         public class InstanceTooltipPropertyConfig : BaseConfigClass
//         {
//             [LabelText("实例类型")]
//             [OnValueChanged(nameof(OnInstanceTypeChangedGUI))]
//             [ValueDropdown(nameof(GetInstanceTypeNameList))]
//             public Type instanceType;
//
//             [LabelText("属性")]
//             [ListDrawerSettings(CustomAddFunction = nameof(AddTooltipPropertyConfigGUI))]
//             [SerializeField]
//             private List<TooltipPropertyConfig> tooltipPropertyConfigs = new();
//
//             #region GUI
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 instanceType ??= typeof(TInstance);
//                 OnInstanceTypeChangedGUI();
//             }
//
//             private void OnInstanceTypeChangedGUI()
//             {
//                 tooltipPropertyConfigs ??= new();
//
//                 foreach (var config in tooltipPropertyConfigs)
//                 {
//                     config.filterType = instanceType;
//                 }
//             }
//
//             private IEnumerable<ValueDropdownItem<Type>> GetInstanceTypeNameList()
//             {
//                 foreach (var instanceType in GetDerivedInstanceTypesWithoutRegisteredID())
//                 {
//                     yield return new ValueDropdownItem<Type>(instanceType.Name,
//                         instanceType);
//                 }
//             }
//
//             private TooltipPropertyConfig AddTooltipPropertyConfigGUI()
//             {
//                 return new TooltipPropertyConfig()
//                 {
//                     filterType = instanceType
//                 };
//             }
//
//             #endregion
//
//             public override void CheckSettings()
//             {
//                 base.CheckSettings();
//
//                 foreach (var tooltipPropertyConfig in tooltipPropertyConfigs)
//                 {
//                     tooltipPropertyConfig.propertyID.AssertIsNotNullOrEmpty(
//                         $"{nameof(tooltipPropertyConfig)}." +
//                         $"{nameof(tooltipPropertyConfig.propertyID)}");
//                 }
//
//                 if (tooltipPropertyConfigs.ContainsSame(
//                         config => config.propertyID))
//                 {
//                     Debug.LogWarning("默认的提示框属性设置有重复的属性ID");
//                 }
//
//                 foreach (var config in tooltipPropertyConfigs)
//                 {
//                     config.CheckSettings();
//                 }
//             }
//
//             protected override void OnInit()
//             {
//                 base.OnInit();
//
//                 foreach (var config in tooltipPropertyConfigs)
//                 {
//                     config.Init();
//                 }
//             }
//
//             public IEnumerable<TooltipPropertyConfigRuntime>
//                 GetTooltipPropertyConfigs()
//             {
//                 foreach (var config in tooltipPropertyConfigs)
//                 {
//                     yield return config.ConvertToRuntime();
//                 }
//             }
//         }
//
//         public class TooltipPropertyConfig : BaseConfigClass
//         {
//             [HideInInspector]
//             public Type filterType;
//
//             [LabelText("属性")]
//             [ValueDropdown(nameof(GetPropertyNameList))]
//             [IsNotNullOrEmpty]
//             public string propertyID;
//
//             [HideInEditorMode]
//             public PropertyConfig propertyConfig;
//
//             [LabelText("是否静态")]
//             public bool isStatic = true;
//
//             #region GUI
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 filterType ??= typeof(TInstance);
//             }
//
//             private IEnumerable GetPropertyNameList()
//             {
//                 return GameCoreSettingBase.propertyGeneralSetting
//                     .GetPropertyNameList(
//                         filterType);
//             }
//
//             #endregion
//
//             public TooltipPropertyConfigRuntime ConvertToRuntime()
//             {
//                 return new TooltipPropertyConfigRuntime()
//                 {
//                     propertyConfig = propertyConfig,
//                     isStatic = isStatic
//                 };
//             }
//
//             protected override void OnInit()
//             {
//                 base.OnInit();
//
//                 propertyConfig = GamePrefabManager.GetGamePrefabStrictly<PropertyConfig>(propertyID);
//
//                 if (propertyConfig.targetType.IsDerivedFrom<TInstance>(true) ==
//                     false)
//                 {
//                     Debug.LogError(
//                         $"属性{propertyID}的目标类型" +
//                         $"{propertyConfig.targetType}与{typeof(TInstance)}不匹配");
//                 }
//             }
//         }
//
//         public struct TooltipPropertyConfigRuntime
//         {
//             public PropertyConfig propertyConfig;
//             public bool isStatic;
//         }
//
//         #endregion
//     }
// }
