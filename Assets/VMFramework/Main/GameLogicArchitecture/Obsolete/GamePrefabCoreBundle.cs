// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using System.IO;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
//
// using JetBrains.Annotations;
//
// using UnityEngine;
//
// using NPOI.SS.UserModel;
// using NPOI.XSSF.UserModel;
//
// using VMFramework.Core;
// using VMFramework.Configuration;
// using Basis.Core;
// using VMFramework.Editor;
//
// using Sirenix.Serialization;
// using Sirenix.OdinInspector;
// using VMFramework.OdinExtensions;
// using VMFramework.Core.Editor;
// using VMFramework.Core.Linq;
// using VMFramework.Localization;
// using Object = UnityEngine.Object;
//
// #if UNITY_EDITOR
//
// using UnityEditor;
//
// #endif
//
// namespace VMFramework.GameLogicArchitecture
// {
//     #region Interface
//
//     public interface IPrefabGeneralSetting
//     {
//         /// <summary>
//         /// 获取特定ID预制体的名称
//         /// 最好仅在Editor模式下调用，因为此方法效率较低
//         /// </summary>
//         /// <param name="id">预制体ID</param>
//         /// <returns></returns>
//         public string GetPrefabName(string id);
//
//         /// <summary>
//         /// 获取所有预制体的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//         /// 比如 [ValueDropdown("@GameSetting.inputSystemGeneralSetting.GetPrefabNameList()")]
//         /// </summary>
//         /// <returns>返回为ValueDropdownItem构成的IEnumerable</returns>
//         public IEnumerable<ValueDropdownItem<string>> GetPrefabNameList();
//
//         /// <summary>
//         /// 获取特定Type对应的类或其派生类的预制体的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//         /// 比如 [ValueDropdown("@GameSetting.uiPanelGeneralSetting.GetPrefabNameList(typeof(ContainerUIPreset))")]
//         /// </summary>
//         /// <param name="specificTypes">约束预制体的类型</param>
//         /// <returns>返回为ValueDropdownItem构成的IEnumerable</returns>
//         public IEnumerable<ValueDropdownItem<string>>
//             GetPrefabNameList(params Type[] specificTypes);
//
//         public string GetGameTypeName(string gameTypeID);
//
//         /// <summary>
//         /// 获取所有叶节点类别的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//         /// </summary>
//         /// <returns></returns>
//         public IEnumerable<ValueDropdownItem<string>> GetGameTypeNameList();
//
//         /// <summary>
//         /// 获取所有类别的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//         /// </summary>
//         /// <returns></returns>
//         public IEnumerable<ValueDropdownItem<string>> GetAllGameTypeNameList();
//
//         public IEnumerable<string> GetAllPrefabsID();
//
//         public IEnumerable<string> GetAllActivePrefabsID();
//     }
//
//     public interface IPrefabConfig
//     {
//
//     }
//
//     #endregion
//
//     public class GamePrefabCoreBundle<TPrefab, TGeneralSetting>
//         where TPrefab : GamePrefabCoreBundle<TPrefab, TGeneralSetting>.GameItemPrefab
//         where TGeneralSetting : GamePrefabCoreBundle<TPrefab, TGeneralSetting>.
//         GameItemGeneralSetting
//     {
//         public const string REGISTERED_ID_NAME = "registeredID";
//         public const string NULL_ID = "null";
//
//         protected static GameType rootGameType { get; private set; }
//
//         #region Extension Utility
//
//         protected static string GetRegisteredID(Type type)
//         {
//             string registeredID = string.Empty;
//
//             if (type.TryGetPropertyByName(REGISTERED_ID_NAME,
//                     out var registeredIDPropertyInfo))
//             {
//                 if (registeredIDPropertyInfo.PropertyType == typeof(string))
//                 {
//                     if (registeredIDPropertyInfo.IsStatic())
//                     {
//                         registeredID = (string)registeredIDPropertyInfo.GetValue(null);
//                     }
//                     else
//                     {
//                         Debug.LogWarning($"{type}的{REGISTERED_ID_NAME}需要为Static");
//                     }
//                 }
//                 else
//                 {
//                     Debug.LogWarning(
//                         $"{type}的{REGISTERED_ID_NAME}的Type需要为string," +
//                         $"而不是{registeredIDPropertyInfo.PropertyType},");
//                 }
//             }
//
//             if (registeredID.IsNullOrEmptyAfterTrim())
//             {
//                 if (type.TryGetFieldByName(REGISTERED_ID_NAME,
//                         out var registeredIDFieldInfo))
//                 {
//                     if (registeredIDFieldInfo.FieldType == typeof(string))
//                     {
//                         if (registeredIDFieldInfo.IsStatic)
//                         {
//                             registeredID = (string)registeredIDFieldInfo
//                                 .GetValue(null);
//                         }
//                         else
//                         {
//                             Debug.LogWarning(
//                                 $"{type}的{REGISTERED_ID_NAME}需要为Static或Const");
//                         }
//                     }
//                     else
//                     {
//                         Debug.LogWarning(
//                             $"{type}的{REGISTERED_ID_NAME}的Type需要为string," +
//                             $"而不是{registeredIDFieldInfo.FieldType},");
//                     }
//                 }
//             }
//
//             return registeredID;
//         }
//
//         public static bool HasRegisteredID(Type type)
//         {
//             return type.HasFieldByName(REGISTERED_ID_NAME) ||
//                    type.HasPropertyByName(REGISTERED_ID_NAME);
//         }
//
//         public static IEnumerable<Type> GetDerivedPrefabsWithoutRegisteredID()
//         {
//             return typeof(TPrefab).GetDerivedClasses(true, false).Where(type =>
//                 HasRegisteredID(type) == false);
//         }
//
//         #endregion
//
//         [HideReferenceObjectPicker]
//         [HideDuplicateReferenceBox]
//         [JsonObject(MemberSerialization.OptIn)]
//         [PreviewComposite]
//         [TypeValidation]
//         public abstract class GameItemPrefab : BaseConfigClass, IIDOwner, INameOwner, IGameTypeOwner,
//             IPrefabConfig, ITypeValidationProvider, ILocalizedStringOwnerConfig
//         {
//             #region Constants
//
//             public const string NULL_ID =
//                 GamePrefabCoreBundle<TPrefab, TGeneralSetting>.NULL_ID;
//
//             protected const string ACTIVE_STATE_AND_DEBUGGING_MODE_HORIZONTAL_GROUP =
//                 "ActiveStateAndDebuggingModeHorizontalGroup";
//
//             protected const string TAB_GROUP_NAME = "TabGroup";
//
//             protected const string BASIC_SETTING_CATEGORY = "基本设置";
//
//             protected const string TOOLS_CATEGORY = "工具";
//
//             protected const string EXTENDED_CLASS_CATEGORY = "扩展类";
//
//             protected const string RUNTIME_DATA_CATEGORY = "运行时数据";
//
//             protected const string OPEN_SCRIPT_CATEGORY =
//                 TAB_GROUP_NAME + "/" + TOOLS_CATEGORY + "/打开脚本";
//
//             #endregion
//
//             public static Dictionary<string, TPrefab> allPrefabsByID = new();
//
//             public static Dictionary<string, HashSet<TPrefab>> allPrefabsByType =
//                 new();
//
//             public static Type prefabType = typeof(TPrefab);
//
//             public static bool gameTypeAbandoned = false;
//
//             private static TGeneralSetting generalSetting;
//
//             /// <summary>
//             /// 推荐的ID后缀
//             /// </summary>
//             protected virtual string preferredIDSuffix => "";
//
//             /// <summary>
//             /// 是否自动添加到预制体列表，需要类里有名为registeredID的Field或者Property
//             /// </summary>
//             public virtual bool autoAddToPrefabList => false;
//
//             protected bool isIDEndsWithPreferredSuffix =>
//                 id.IsNullOrEmptyAfterTrim() ||
//                 preferredIDSuffix.IsNullOrEmptyAfterTrim() ||
//                 id.TrimEnd(' ', '_').EndsWith("_" + preferredIDSuffix);
//
//
//             #region Configs
//
//             [LabelText("ID", SdfIconType.Globe)]
//             [IsNotNullOrEmpty(DrawCurrentRect = true)]
//             [ValidateIsNot(content:NULL_ID, DrawCurrentRect = true)]
//             [Placeholder("@" + nameof(GetIDPlaceholderText) + "()")]
//             [OnValueChanged(nameof(OnIDChangedGUI))]
//             [JsonProperty(Order = -10000), PropertyOrder(-10000)]
//             public string id;
//
//             [LabelText("是否启用", SdfIconType.Activity)]
//             [JsonProperty(Order = -9000), PropertyOrder(-9000)]
//             [HorizontalGroup(ACTIVE_STATE_AND_DEBUGGING_MODE_HORIZONTAL_GROUP)]
//             public bool isActive = true;
//
//             [LabelText("调试模式", SdfIconType.BugFill)]
//             [JsonProperty(Order = -8000), PropertyOrder(-8000)]
//             [HorizontalGroup(ACTIVE_STATE_AND_DEBUGGING_MODE_HORIZONTAL_GROUP)]
//             public bool isDebugging = false;
//
//             [LabelText("游戏种类"),
//              TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY, SdfIconType.Info,
//                  TextColor = "blue")]
//             [JsonProperty(Order = -6000), PropertyOrder(-6000)]
//             [ValueDropdown(nameof(GetTypeNameList))]
//             [IsNotNullOrEmpty]
//             [HideIf(nameof(gameTypeAbandoned))]
//             [ListDrawerSettings(ShowFoldout = false)]
//             [SerializeField]
//             protected List<string> _initialGameTypesID = new();
//
//             public IReadOnlyList<string> initialGameTypesID
//             {
//                 get => _initialGameTypesID;
//                 init => _initialGameTypesID = value.ToList();
//             }
//
//             #endregion
//
//             #region GUI
//
//             private bool isCurrentTypeNotEqualsToPrefabType =>
//                 GetType() != prefabType;
//
//             protected override void OnInspectorInit()
//             {
//                 //generalSetting = GetGeneralSetting();
//
//                 id ??= "";
//
//                 CheckGameTypeIfAbandoned();
//
//                 if (gameTypeAbandoned == false)
//                 {
//                     _initialGameTypesID ??= new();
//
//                     if (_initialGameTypesID.Count == 0)
//                     {
//                         _initialGameTypesID.Add(GetStaticGeneralSetting()
//                             .defaultTypeID);
//                     }
//                 }
//                 
//                 name ??= new LocalizedStringReference();
//             }
//
//             #region Id Changed
//
//             protected virtual void OnIDChangedGUI()
//             {
//                 
//             }
//
//             #endregion
//
//             #region Drop Down Item
//
//             public ValueDropdownItem<string> GetNameIDDropDownItem()
//             {
//                 return new ValueDropdownItem<string>(name, id);
//             }
//
//             #endregion
//
//             #region ID Tool
//
//             private string GetIDPlaceholderText()
//             {
//                 const string placeholderText = "请输入ID";
//                 if (preferredIDSuffix.IsNullOrEmptyAfterTrim())
//                 {
//                     return placeholderText;
//                 }
//
//                 return placeholderText + $"并以_{preferredIDSuffix}结尾";
//             }
//
//             [Button("补全ID结尾")]
//             [HideIf(nameof(isIDEndsWithPreferredSuffix))]
//             [PropertyOrder(-10000)]
//             private void CompleteIDSuffix()
//             {
//                 if (id.IsNullOrEmptyAfterTrim())
//                 {
//                     id = "";
//                 }
//
//                 if (id.EndsWith("_") == false)
//                 {
//                     id += "_";
//                 }
//
//                 id += preferredIDSuffix;
//             }
//
//             [Button("使用ID作为名称"), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
//             private void ReplaceNameByID()
//             {
//                 name.SetKeyValueInEditor(id.ToPascalCase(" "), false);
//             }
//
//             #endregion
//
//             #region Change Type
//
//             protected virtual bool showChangeTypeButton => true;
//
//             public virtual void OnTypeChanged()
//             {
//
//             }
//
//             [Button("改变类型"), TabGroup(TAB_GROUP_NAME, EXTENDED_CLASS_CATEGORY)]
//             [ShowIf(nameof(showChangeTypeButton))]
//             [PropertyOrder(1000)]
//             private void ChangeType()
//             {
//                 if (TryGetStaticGeneralSetting(out var setting))
//                 {
//                     setting.InvokeMethodByName("ChangeType");
//                 }
//                 else
//                 {
//                     Note.note.Error("没有找到对应的通用用设置");
//                 }
//             }
//
//             #endregion
//
//             #region Open Script
//
// #if UNITY_EDITOR
//             [Button("打开预制体脚本"),
//              TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY, SdfIconType.Tools,
//                  TextColor = "yellow"), 
//              HorizontalGroup(OPEN_SCRIPT_CATEGORY)]
//             private void OpenPrefabScript()
//             {
//                 GetType().OpenScriptOfType();
//             }
// #endif
//
//             #endregion
//
//             #region Type Validation Provider
//
// #if UNITY_EDITOR
//             IEnumerable<ValidationResult> ITypeValidationProvider.
//                 GetValidationResults(GUIContent label)
//             {
//                 if (id.IsNullOrEmpty())
//                 {
//                     yield return new("ID不能为空",
//                         ValidateType.Error);
//                 }
//                 else
//                 {
//                     if (id.Contains(' '))
//                     {
//                         yield return new("ID不能有空格", ValidateType.Error);
//                     }
//
//                     if (id == NULL_ID)
//                     {
//                         yield return new($"ID不能为{NULL_ID}", ValidateType.Error);
//                     }
//
//                     if (isIDEndsWithPreferredSuffix == false)
//                     {
//                         yield return new($"ID最好以_{preferredIDSuffix}结尾", ValidateType.Warning);
//                     }
//                 }
//
//                 if (gameTypeAbandoned == false && _initialGameTypesID.IsNullOrEmpty())
//                 {
//                     yield return new("游戏种类不能为空", ValidateType.Error);
//                 }
//             }
// #endif
//
//             #endregion
//
//             #endregion
//
//             #region Localization
//
//             [LabelText("名称", SdfIconType.FileEarmarkPersonFill),
//              TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
//             [JsonProperty(Order = -5000), PropertyOrder(-5000)]
//             [CustomContextMenu("使用ID作为名称", nameof(ReplaceNameByID))]
//             public LocalizedStringReference name = new();
//
//             public virtual void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
//             {
//                 name ??= new();
//                 
//                 name.AutoConfigNameByID(id, settings.defaultTableName);
//             }
//
//             public virtual void CreateLocalizedStringKeys()
//             {
//                 AutoConfigureLocalizedString(default);
//                 name.CreateNewKey();
//             }
//
//             public void SetKeyValueByDefault()
//             {
//                 name.SetKeyValueByDefault();
//             }
//
//             #endregion
//
//             #region Init & Check
//
//             public static event Action<TPrefab> OnPreInitEvent;
//             public static event Action<TPrefab> OnInitEvent;
//             public static event Action<TPrefab> OnPostInitEvent;
//
//             public void PreInit()
//             {
//                 Note.note.Begin($"预加载{this}");
//                 OnPreInit();
//                 Note.note.End();
//
//                 OnPreInitEvent?.Invoke((TPrefab)this);
//             }
//
//             public override void Init()
//             {
//                 base.Init();
//
//                 OnInitEvent?.Invoke((TPrefab)this);
//             }
//
//             public void PostInit()
//             {
//                 Note.note.Begin($"后加载{this}");
//                 OnPostInit();
//                 Note.note.End();
//
//                 OnPostInitEvent?.Invoke((TPrefab)this);
//             }
//
//             protected virtual void OnPreInit()
//             {
//                 if (allPrefabsByID.ContainsKey(id))
//                 {
//                     Debug.LogWarning($"已存在相同id");
//                 }
//
//                 allPrefabsByID[id] = (TPrefab)this;
//             }
//
//             /// <summary>
//             /// 当加载此预制体时调用
//             /// </summary>
//             protected override void OnInit()
//             {
//                 CheckSettings();
//
//                 if (gameTypeAbandoned == false)
//                 {
//                     _gameTypeSet = new(this);
//
//                     _gameTypeSet.OnAddGameType += OnAddGameType;
//                     _gameTypeSet.OnAddLeafGameType += OnAddLeafGameType;
//
//                     _gameTypeSet.OnRemoveGameType += OnRemoveGameType;
//                     _gameTypeSet.OnRemoveLeafGameType += OnRemoveLeafGameType;
//
//                     _gameTypeSet.AddGameTypes(initialGameTypesID);
//                 }
//             }
//
//             protected virtual void OnPostInit()
//             {
//
//             }
//
//             #region Check Settings
//
//             public override void CheckSettings()
//             {
//                 if (id.IsNullOrEmptyAfterTrim())
//                 {
//                     Note.note.Error($"id不能为空");
//                 }
//
//                 if (id == NULL_ID)
//                 {
//                     Note.note.Error($"id不能为 {NULL_ID}");
//                 }
//
//                 if (isIDEndsWithPreferredSuffix == false)
//                 {
//                     Debug.LogWarning($"id：{id}最好以 _{preferredIDSuffix} 结尾");
//                 }
//
//                 if (gameTypeAbandoned == false)
//                 {
//                     var generalSetting = GetStaticGeneralSettingStrictly();
//
//                     _initialGameTypesID ??= new();
//
//                     if (initialGameTypesID.Count == 0)
//                     {
//                         _initialGameTypesID.Add(generalSetting.defaultTypeID);
//                     }
//
//                     if (initialGameTypesID.Count > 1 &&
//                         generalSetting.isTypeIDUnique)
//                     {
//                         Debug.LogWarning("种类ID不唯一");
//                     }
//
//                     if (initialGameTypesID.ContainsSame())
//                     {
//                         Debug.LogWarning("种类ID有重复");
//                     }
//                 }
//             }
//
//             #endregion
//
//             #endregion
//
//             #region GameType
//
//             [LabelText("游戏种类集合"),
//              TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY, SdfIconType.Bug,
//                  TextColor = "orange")]
//             [ShowInInspector]
//             [HideInEditorMode]
//             private GameTypeSet _gameTypeSet;
//
//             public GameTypeSet gameTypeSet
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get
//                 {
//                     //if (gameTypeAbandoned)
//                     //{
//                     //    Debug.LogWarning($"此预制体{this}已经废弃了游戏种类，获取游戏种类无效");
//                     //    return null;
//                     //}
//
//                     return _gameTypeSet;
//                 }
//             }
//
//             [LabelText("唯一种类"), TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
//             [ShowInInspector]
//             [HideInEditorMode]
//             [JsonIgnore]
//             public GameType uniqueGameType { get; private set; }
//
//             private void OnAddGameType(IReadOnlyGameTypeSet gameTypeSet, GameType gameType)
//             {
//                 allPrefabsByType.TryAdd(gameType.id, new HashSet<TPrefab>());
//
//                 allPrefabsByType[gameType.id].Add((TPrefab)this);
//             }
//
//             private void OnAddLeafGameType(IReadOnlyGameTypeSet gameTypeSet, GameType gameType)
//             {
//                 uniqueGameType ??= gameType;
//             }
//
//             private void OnRemoveGameType(IReadOnlyGameTypeSet gameTypeSet, GameType gameType)
//             {
//                 allPrefabsByType[gameType.id].Remove((TPrefab)this);
//             }
//
//             private void OnRemoveLeafGameType(IReadOnlyGameTypeSet gameTypeSet, GameType gameType)
//             {
//                 if (uniqueGameType == gameType)
//                 {
//                     uniqueGameType = null;
//
//                     if (_gameTypeSet.HasGameType())
//                     {
//                         uniqueGameType = _gameTypeSet.leafGameTypes.Choose();
//                     }
//                 }
//             }
//
//             #endregion
//
//             #region GetGeneralSetting
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             internal static void SetStaticGeneralSetting(
//                 TGeneralSetting newGeneralSetting)
//             {
//                 Note.note.AssertIsNotNull(newGeneralSetting,
//                     nameof(newGeneralSetting));
//                 generalSetting = newGeneralSetting;
//             }
//
//             /// <summary>
//             /// 获得此预制体对应的通用设置，Editor和Runtime下调用皆可
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static TGeneralSetting GetStaticGeneralSetting()
//             {
//                 if (generalSetting == null)
//                 {
//                     return GameCoreSettingBase.FindGeneralSetting<TGeneralSetting>();
//                 }
//
//                 return generalSetting;
//             }
//
//             /// <summary>
//             /// 获得此预制体对应的通用设置，Editor和Runtime下调用皆可，如果没有找到则会报错
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             [NotNull]
//             public static TGeneralSetting GetStaticGeneralSettingStrictly()
//             {
//                 var setting = GetStaticGeneralSetting();
//
//                 if (setting == null)
//                 {
//                     Note.note.Error("找不到通用设置");
//                 }
//
//                 return setting;
//             }
//
//             /// <summary>
//             /// 尝试获得此预制体对应的通用设置，Editor和Runtime下调用皆可
//             /// </summary>
//             /// <param name="setting">获得的通用设置</param>
//             /// <returns>尝试结果</returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static bool TryGetStaticGeneralSetting(
//                 out TGeneralSetting setting)
//             {
//                 if (generalSetting == null)
//                 {
//                     setting =
//                         GameCoreSettingBase.FindGeneralSetting<TGeneralSetting>();
//                 }
//                 else
//                 {
//                     setting = generalSetting;
//                 }
//
//                 return setting != null;
//             }
//
//             #endregion
//
//             #region Get Prefab
//
//             public static bool TryGetPrefab(string id, out TPrefab prefab)
//             {
//                 return allPrefabsByID.TryGetValue(id, out prefab);
//             }
//
//             public static bool TryGetPrefab<T>(string id, out T prefab)
//                 where T : TPrefab
//             {
//                 if (allPrefabsByID.TryGetValue(id, out var targetPrefab))
//                 {
//                     prefab = targetPrefab as T;
//                     return prefab != null;
//                 }
//
//                 prefab = null;
//                 return false;
//             }
//
//             /// <summary>
//             /// 通过ID获得预制体，如果没有找到则会警告
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             public static TPrefab GetPrefab(string id)
//             {
//                 if (allPrefabsByID.TryGetValue(id, out var targetPrefab))
//                 {
//                     return targetPrefab;
//                 }
//
//                 Debug.LogWarning($"不存在id为{id}的{typeof(TPrefab)}");
//                 return null;
//             }
//
//             public static T GetPrefab<T>(string id) where T : TPrefab
//             {
//                 return GetPrefab(id) as T;
//             }
//
//             /// <summary>
//             /// 通过ID获得预制体，如果没有找到则会报错
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             public static TPrefab GetPrefabStrictly(string id)
//             {
//                 if (id == null)
//                 {
//                     throw new ArgumentNullException(nameof(id));
//                 }
//
//                 if (allPrefabsByID.TryGetValue(id, out var targetPrefab))
//                 {
//                     return targetPrefab;
//                 }
//
//                 throw new ArgumentException($"不存在id为{id}的{typeof(TPrefab)}");
//             }
//
//             public static T GetPrefabStrictly<T>(string id) where T : TPrefab
//             {
//                 var prefab = GetPrefabStrictly(id);
//
//                 if (prefab is T t)
//                 {
//                     return t;
//                 }
//
//                 Note.note.Error($"id为{id}的{typeof(TPrefab)}类型不是{typeof(T)}");
//                 return null;
//             }
//
//             public static TPrefab GetActivePrefab(string id)
//             {
//                 var prefab = GetPrefab(id);
//
//                 if (prefab == null)
//                 {
//                     return null;
//                 }
//
//                 if (prefab.isActive == false)
//                 {
//                     Debug.LogWarning($"id为{id}的{typeof(TPrefab)}未启用");
//                     return null;
//                 }
//
//                 return prefab;
//             }
//
//             public static T GetActivePrefab<T>(string id) where T : TPrefab
//             {
//                 var prefab = GetActivePrefab(id);
//
//                 if (prefab is T t)
//                 {
//                     return t;
//                 }
//
//                 Note.note.Error($"id为{id}的{typeof(TPrefab)}类型不是{typeof(T)}");
//                 return null;
//             }
//
//             /// <summary>
//             /// 获得随机的预制体
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static TPrefab GetRandomPrefab()
//             {
//                 return allPrefabsByID.Values.Choose();
//             }
//
//             /// <summary>
//             /// 获取所有预制体
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static IEnumerable<TPrefab> GetAllPrefabs()
//             {
//                 return generalSetting.GetAllPrefabs();
//             }
//
//             /// <summary>
//             /// 获取所有启用的预制体
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static IEnumerable<TPrefab> GetAllActivePrefabs()
//             {
//                 return generalSetting.GetAllPrefabs()
//                     .Where(prefab => prefab.isActive);
//             }
//
//             /// <summary>
//             /// 获取所有预制体，但是不保证顺序
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static IEnumerable<TPrefab> GetAllPrefabsUnordered()
//             {
//                 return allPrefabsByID.Values;
//             }
//
//
//             /// <summary>
//             /// 获取所有启用的预制体，但不保证顺序
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static IEnumerable<TPrefab> GetAllActivePrefabsUnordered()
//             {
//                 return allPrefabsByID.Values.Where(prefab => prefab.isActive);
//             }
//
//             /// <summary>
//             /// 获取所有预制体
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static IReadOnlyList<T> GetAllPrefabs<T>() where T : TPrefab
//             {
//                 return GetAllPrefabs().Where(prefab => prefab is T).Cast<T>()
//                     .ToList();
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static TPrefab GetFirstPrefab()
//             {
//                 return generalSetting.GetAllPrefabs().FirstOrDefault();
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static T GetFirstPrefab<T>() where T : TPrefab
//             {
//                 return generalSetting.GetAllPrefabs()
//                     .FirstOrDefault(prefab => prefab is T) as T;
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static TPrefab GetLastPrefab()
//             {
//                 return generalSetting.GetAllPrefabs().LastOrDefault();
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static T GetLastPrefab<T>() where T : TPrefab
//             {
//                 return generalSetting.GetAllPrefabs()
//                     .LastOrDefault(prefab => prefab is T) as T;
//             }
//
//             /// <summary>
//             /// 从通用设置里用ID获取预制体，一般用于在Editor模式下调用，效率较低
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             public static TPrefab GetPrefabFromGeneralSetting(string id)
//             {
//                 if (TryGetStaticGeneralSetting(out var generalSetting))
//                 {
//                     return generalSetting.GetPrefabStrictly(id);
//                 }
//
//                 return null;
//             }
//
//             #endregion
//
//             #region Get Prefab ID
//
//             public static IEnumerable<string> GetAllPrefabsID()
//             {
//                 return generalSetting.GetAllPrefabsID();
//             }
//
//             public static IEnumerable<string> GetAllPrefabsIDUnordered()
//             {
//                 return allPrefabsByID.Keys;
//             }
//
//             #endregion
//
//             #region Contains Prefab
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static bool ContainsPrefabID(string id)
//             {
//                 return allPrefabsByID.ContainsKey(id);
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public static bool ContainsActivePrefabID(string id)
//             {
//                 var prefab = GetPrefab(id);
//
//                 return prefab is { isActive: true };
//             }
//
//             #endregion
//
//             #region GetPrefabByType
//
//             /// <summary>
//             /// 所有预制体里是否包含特定种类ID的预制体
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public static bool ContainsGameType(string gameTypeID) =>
//                 allPrefabsByType.ContainsKey(gameTypeID);
//
//             /// <summary>
//             /// 所有预制体里是否包含特定种类ID的预制体，如果没有会报错
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public static bool ContainsGameTypeStrictly(string gameTypeID)
//             {
//                 bool result = ContainsGameType(gameTypeID);
//                 if (result == false)
//                 {
//                     Note.note.Error($"没有预制体为此typeID:{gameTypeID}");
//                 }
//
//                 return result;
//             }
//
//             /// <summary>
//             /// 随机获得一个特定种类的预制体
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public static TPrefab GetRandomPrefabByGameType(string gameTypeID)
//             {
//                 return allPrefabsByType.ContainsKey(gameTypeID)
//                     ? allPrefabsByType[gameTypeID].Choose()
//                     : null;
//             }
//
//             /// <summary>
//             /// 随机获得一个特定种类的预制体ID
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public static string GetRandomPrefabIDByGameType(string gameTypeID)
//             {
//                 GameItemPrefab randomPrefab = GetRandomPrefabByGameType(gameTypeID);
//
//                 return randomPrefab == null ? "" : randomPrefab.id;
//             }
//
//             /// <summary>
//             /// 获取特定种类的所有预制体
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public static IEnumerable<TPrefab> GetPrefabsByGameType(
//                 string gameTypeID)
//             {
//                 if (allPrefabsByType.TryGetValue(gameTypeID, out var prefabs))
//                 {
//                     return prefabs;
//                 }
//
//                 return Enumerable.Empty<TPrefab>();
//             }
//
//             /// <summary>
//             /// 获取特定种类的所有预制体ID
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public static IEnumerable<string> GetPrefabsIDByGameType(
//                 string gameTypeID)
//             {
//                 return GetPrefabsByGameType(gameTypeID).Select(prefab => prefab.id);
//             }
//
//             #endregion
//
//             #region Type
//
//             private static void CheckGameTypeIfAbandoned()
//             {
//                 var generalSetting = GetStaticGeneralSetting();
//
//                 Note.note.AssertIsNotNull(generalSetting, nameof(generalSetting));
//
//                 gameTypeAbandoned = generalSetting.gameTypeAbandoned;
//             }
//
//             private IEnumerable GetTypeNameList()
//             {
//                 var generalSetting = GetStaticGeneralSetting();
//
//                 if (generalSetting == null)
//                 {
//                     Debug.LogWarning("找不到通用设置");
//                     return null;
//                 }
//
//                 return generalSetting.GetGameTypeNameList();
//             }
//
//             #endregion
//
//             #region Extension
//
//             public static Dictionary<string, Type> extendedPrefabTypes = new();
//
//             [LabelText("扩展预制体类"),
//              TabGroup(TAB_GROUP_NAME, EXTENDED_CLASS_CATEGORY, SdfIconType.Code,
//                  TextColor = "green")]
//             [ShowInInspector]
//             [HideIfNull]
//             [PropertyOrder(-7000)]
//             [EnableGUI]
//             public Type extendedPrefabType => GetExtendedPrefabType(id);
//
//             [LabelText("当前类型"), TabGroup(TAB_GROUP_NAME, EXTENDED_CLASS_CATEGORY)]
//             [EnableGUI]
//             [ShowInInspector]
//             [ShowIf(nameof(isCurrentTypeNotEqualsToPrefabType))]
//             private Type currentType => GetType();
//
//             public static Type GetExtendedPrefabType(string id)
//             {
//                 if (id.IsNullOrEmptyAfterTrim())
//                 {
//                     return null;
//                 }
//
//                 return extendedPrefabTypes.TryGetValue(id, out var type) ? type : null;
//             }
//
//             public static void RefreshExtendedPrefabs()
//             {
//                 if (TryGetStaticGeneralSetting(out var generalSetting) == false)
//                 {
//                     Debug.LogWarning(
//                         $"找不到{typeof(TPrefab)}对应的通用设置类型:{typeof(TGeneralSetting)}");
//                     return;
//                 }
//
//                 extendedPrefabTypes.Clear();
//
//                 foreach (Type currentPrefabType in
//                          typeof(TPrefab).GetDerivedClasses(false, false))
//                 {
//                     if (currentPrefabType.IsAbstract)
//                     {
//                         continue;
//                     }
//
//                     var registeredID = GetRegisteredID(currentPrefabType);
//
//                     if (registeredID.IsNullOrEmptyAfterTrim())
//                     {
//                         continue;
//                     }
//
//                     if (extendedPrefabTypes.TryGetValue(registeredID, out var extendedType))
//                     {
//                         if (currentPrefabType.IsDerivedFrom(extendedType, false) ==
//                             false)
//                         {
//                             Debug.LogWarning($"[扩展{{type}}]" +
//                                              $"{currentPrefabType.FullName}重复" +
//                                              $"注册扩展预制体id:{registeredID}，" +
//                                              $"{extendedType.FullName}原先注册的扩展预制体被覆盖");
//                         }
//                     }
//
//                     if (generalSetting.ContainsPrefabID(registeredID) == false)
//                     {
//                         var prefabInstance =
//                             currentPrefabType.CreateInstance() as TPrefab;
//
//                         if (prefabInstance.autoAddToPrefabList)
//                         {
//                             prefabInstance.id = registeredID;
//                             if (generalSetting.TryAddPrefab(prefabInstance,
//                                     out var newPrefab))
//                             {
//                                 Debug.Log(
//                                     $"自动添加扩展预制体:{newPrefab}到通用设置里");
//                             }
//                         }
//                         else
//                         {
//                             Debug.LogWarning($"通用设置里不包含此ID:{registeredID}的预制体，" +
//                                              $"无法注册扩展预制体:{currentPrefabType.FullName}");
//                             continue;
//                         }
//                     }
//
//                     extendedPrefabTypes[registeredID] = currentPrefabType;
//                 }
//             }
//
//             #endregion
//
//             #region To String
//
//             public override string ToString()
//             {
//                 return $"({GetType()}, id:{id})";
//             }
//
//             #endregion
//
//             #region ID Owner & Name Owner & Game Type Owner
//
//             string IIDOwner<string>.id => id;
//
//             string INameOwner.name => name;
//
//             IGameTypeSet IGameTypeOwner.gameTypeSet => gameTypeSet;
//
//             #endregion
//
//             public static void InitStatic()
//             {
//                 CheckGameTypeIfAbandoned();
//
//                 Debug.Log($"正在加载{typeof(TPrefab)}的预制体扩展");
//
//                 RefreshExtendedPrefabs();
//
//                 Debug.Log($"一共加载了{extendedPrefabTypes.Count}个自定义预制体扩展类");
//
//                 Debug.Log($"{typeof(TPrefab)}的预制体扩展加载结束");
//             }
//         }
//
//         #region Game Type Basic Info
//
//         [HideDuplicateReferenceBox]
//         [HideReferenceObjectPicker]
//         [Serializable]
//         public sealed class GameTypeBasicInfo : BaseConfigClass,
//             IChildrenProvider<GameTypeBasicInfo>, ILocalizedStringOwnerConfig
//         {
//             [LabelText("ID")]
//             [IsNotNullOrEmpty]
//             [PropertyOrder(-9000)]
//             public string id;
//
//             [LabelText("名称")]
//             [PropertyOrder(-5000)]
//             public LocalizedStringReference name = new();
//
//             [LabelText("子种类")]
//             [ListDrawerSettings(CustomAddFunction = nameof(AddSubtypeGUI))]
//             public List<GameTypeBasicInfo> subtypes = new();
//
//             [HideInInspector]
//             public string parentID;
//
//             #region Tree Node
//
//             public IEnumerable<GameTypeBasicInfo> GetChildren() => subtypes;
//
//             #endregion
//
//             #region GUI
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 name ??= new();
//             }
//
//             private GameTypeBasicInfo AddSubtypeGUI()
//             {
//                 return new();
//             }
//
//             [Button("使用ID作为名称")]
//             [PropertyOrder(-4500)]
//             private void ReplaceNameByID()
//             {
//                 name.SetKeyValueInEditor(id.ToPascalCase(" "), false);
//             }
//
//             #endregion
//
//             #region Localization
//
//             public void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
//             {
//                 name ??= new();
//                 
//                 name.AutoConfigNameByID(id, settings.defaultTableName);
//             }
//
//             public void CreateLocalizedStringKeys()
//             {
//                 AutoConfigureLocalizedString(default);
//                 name.CreateNewKey();
//             }
//
//             public void SetKeyValueByDefault()
//             {
//                 name.SetKeyValueByDefault();
//             }
//
//             #endregion
//         }
//
//         #endregion
//
//         [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
//         public abstract class GameItemGeneralSetting : GeneralSettingBase,
//             IPrefabGeneralSetting, IGameEditorMenuTreeNodesProvider, ILocalizedStringOwnerConfig
//         {
//             #region Constants
//
//             public const string NULL_ID =
//                 GamePrefabCoreBundle<TPrefab, TGeneralSetting>.NULL_ID;
//
//             protected const string EXTENDED_TYPES_SETTING_CATEGORY = "扩展类";
//
//             protected const string GAME_TYPE_SETTING_CATEGORY = "种类设置";
//
//             protected const string MISCELLANEOUS_SETTING_CATEGORY = "杂项设置";
//
//             protected const string QUERY_PREFABS_CATEGORY = "查询预制体";
//
//             protected const string TEST_CATEGORY = "测试";
//
//             #endregion
//
//             [LabelText("Excel存储路径")]
//             [GUIColor(0.906f, 0.635f, 0.227f)]
//             [ShowInInspector, DisplayAsString, PropertyOrder(-995)]
//             [EnableGUI, LabelWidth(80)]
//             public string excelFilePath
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get =>
//                     dataFolderPath.PathCombine(
//                         $"{GetType().GetNiceName()}.xlsx");
//             }
//
//             public string prefabFullName => prefabName + prefabSuffixName;
//
//             /// <summary>
//             /// 设置里预制体的名字
//             /// </summary>
//             public virtual LocalizedTempString prefabName => new LocalizedTempString()
//             {
//                 { "en-US", "未注册预制体名称" },
//                 { "zh-CN", "Unregistered Prefab Name" }
//             };
//
//             /// <summary>
//             /// 设置里预制体的后缀名称
//             /// </summary>
//             public virtual string prefabSuffixName => new LocalizedTempString()
//             {
//                 { "en-US", "Prefab" },
//                 { "zh-CN", "预制体" }
//             };
//
//             /// <summary>
//             /// 预制体列表里最少需要的预制体，否则GUI会弹出警告
//             /// </summary>
//             public virtual int minPrefabCount => 0;
//
//             /// <summary>
//             /// 添加到预制体列表里的Prefab的Type需求
//             /// </summary>
//             protected virtual Type requirePrefabType => typeof(TPrefab);
//
//             /// <summary>
//             /// 是否显示将第一个预制体设置成默认ID和Name的按钮，仅测试用
//             /// </summary>
//             protected virtual bool showSetFirstPrefabIDAndNameToDefaultButton =>
//                 true;
//
//             [LabelText("扩展预制体类"), TabGroup(TAB_GROUP_NAME, EXTENDED_TYPES_SETTING_CATEGORY,
//                  SdfIconType.Code, TextColor = "green")]
//             [ReadOnly]
//             [EnableGUI]
//             [ShowInInspector]
//             [PropertyOrder(-100)]
//             protected Dictionary<string, Type> extendedPrefabTypes =>
//                 GameItemPrefab.extendedPrefabTypes;
//
//             [LabelText("是否弃用种类设置"), TabGroup(TAB_GROUP_NAME, GAME_TYPE_SETTING_CATEGORY,
//                  SdfIconType.Tree, TextColor = "red")]
//             [ShowInInspector]
//             public virtual bool gameTypeAbandoned => false;
//
//             /// <summary>
//             /// 种类树次根的ID
//             /// </summary>
//             //public virtual string SUBROOT_TYPE_ID => "SUBROOT";
//             [LabelText(@"@prefabName + ""种类"""),
//              TabGroup(TAB_GROUP_NAME, GAME_TYPE_SETTING_CATEGORY)]
//             [NonSerialized, OdinSerialize]
//             [HideIf(nameof(gameTypeAbandoned))]
//             protected GameTypeBasicInfo typeBasicInfo = new();
//
//             /// <summary>
//             /// 默认种类ID
//             /// </summary>
//             [LabelText("默认种类"), TabGroup(TAB_GROUP_NAME, GAME_TYPE_SETTING_CATEGORY)]
//             [ValueDropdown(nameof(GetGameTypeNameList))]
//             [IsNotNullOrEmpty]
//             [HideIf(nameof(gameTypeAbandoned))]
//             public string defaultTypeID;
//
//             [LabelText(@"@prefabName + ""种类唯一"""), 
//              TabGroup(TAB_GROUP_NAME, GAME_TYPE_SETTING_CATEGORY)]
//             [HideIf(nameof(gameTypeAbandoned))]
//             public bool isTypeIDUnique = true;
//
//             [LabelText("@" + nameof(allGameItemPrefabsGUIName)),
//              TabGroup(TAB_GROUP_NAME, "@" + nameof(allGameItemPrefabsGUIName),
//                  SdfIconType.Info, TextColor = "blue")]
//             [InfoBox("ID重复", InfoMessageType.Error, nameof(ContainsSameID))]
//             [InfoBox("存在空ID", InfoMessageType.Error, nameof(ContainsEmptyID))]
//             [InfoBox("存在Null", InfoMessageType.Error, nameof(ContainsNullPrefab))]
//             [ValidateListLength(nameof(minPrefabCount), int.MaxValue,
//                 ValidateType = ValidateType.Warning)]
//             [EnumerableValidation]
//             [OnCollectionChanged(nameof(OnAllGameItemPrefabsChanged))]
//             [PropertyOrder(99)]
//             [ListItemSelector(nameof(AllGameItemPrefabsListSelectHandle),
//                 0.9f, 0.9f, 1f, 0.12f)]
//             [ListDrawerSettings(NumberOfItemsPerPage = 4)]
//             [Searchable]
//             [SerializeField, JsonProperty]
//             protected List<TPrefab> allGameItemPrefabs = new();
//
//             #region Localization
//
//             public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
//             {
//                 base.AutoConfigureLocalizedString(settings);
//                 
//                 allGameItemPrefabs.Examine(prefab =>
//                     prefab?.AutoConfigureLocalizedString(settings));
//
//                 foreach (var info in typeBasicInfo.PreorderTraverse(true))
//                 {
//                     info.AutoConfigureLocalizedString(settings);
//                 }
//             }
//
//             public override void CreateLocalizedStringKeys()
//             {
//                 base.CreateLocalizedStringKeys();
//                 
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.CreateLocalizedStringKeys();
//                 }
//
//                 if (gameTypeAbandoned == false)
//                 {
//                     foreach (var info in typeBasicInfo.PreorderTraverse(true))
//                     {
//                         info.CreateLocalizedStringKeys();
//                     }
//                 }
//             }
//
//             #endregion
//             
//             #region GUI
//
//             private bool isPrefabsCountLessThanMinCount =>
//                 allGameItemPrefabs != null &&
//                 allGameItemPrefabs.Count < minPrefabCount;
//
//             private string allGameItemPrefabsGUIName =>
//                 prefabName + prefabSuffixName;
//
//             #region Inspector Init & Check
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 allGameItemPrefabs ??= new();
//
//                 //Init Type Info
//                 CheckGameTypeInfo();
//
//                 //Prefab Extension
//                 GameItemPrefab.RefreshExtendedPrefabs();
//
//                 for (int i = 0; i < allGameItemPrefabs.Count; i++)
//                 {
//                     var currentPrefab = allGameItemPrefabs[i];
//
//                     if (currentPrefab.extendedPrefabType == null)
//                     {
//                         continue;
//                     }
//
//                     if (currentPrefab.GetType() != currentPrefab.extendedPrefabType)
//                     {
//                         allGameItemPrefabs[i] =
//                             currentPrefab.ConvertToChildOrParent(currentPrefab
//                                 .extendedPrefabType);
//                         allGameItemPrefabs[i].OnTypeChanged();
//                     }
//                 }
//             }
//
//             //protected virtual void OnDrawPrefabListButton()
//             //{
//             //    if (SirenixEditorGUI.ToolbarButton(EditorIcons.Plus))
//             //    {
//             //        AddNewPrefabGUI();
//             //    }
//             //}
//
//             //protected virtual void AddNewPrefabGUI()
//             //{
//             //    var typeSelector = new TypeSelector(GetDerivedPrefabsWithoutRegisteredID(), false);
//
//             //    typeSelector.SetSelection(typeof(Prefab));
//             //    typeSelector.SelectionConfirmed += types =>
//             //    {
//             //        var type = types.FirstOrDefault();
//             //        allGameItemPrefabs.Add(type.CreateInstance() as Prefab);
//             //    };
//
//             //    typeSelector.ShowInPopup(450);
//             //}
//
//             public override void CheckSettingsGUI()
//             {
//                 base.CheckSettingsGUI();
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     Note.note.Begin($"检查{prefab}");
//                     prefab.CheckSettings();
//                     Note.note.End();
//                 }
//             }
//
//             #endregion
//
//             #region On All Game Item Prefabs Changed
//
//             protected virtual void OnAllGameItemPrefabsChanged()
//             {
//                 allGameItemPrefabs.RemoveAllNull();
//
//                 foreach (var prefab in allGameItemPrefabs.ToArray())
//                 {
//                     if (prefab.GetType()
//                             .IsDerivedFrom(requirePrefabType, true, true) ==
//                         false)
//                     {
//                         Debug.LogWarning(
//                             $"{prefab.GetType().Name}不是所需类:{requirePrefabType.Name}或其子类");
//                         allGameItemPrefabs.Remove(prefab);
//                     }
//                 }
//                 
//                 AutoConfigureLocalizedString(new()
//                 {
//                     defaultTableName = defaultLocalizationTableName,
//                     save = true,
//                 });
//             }
//
//             #endregion
//
//             #region Test Tools
//
//             [Button(@"@""移除ID重复的"" + " + nameof(prefabFullName)),
//              TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
//             [ShowIf(nameof(ContainsSameID))]
//             private void RemoveDuplicateIDPrefabs()
//             {
//                 var tempPrefabs = allGameItemPrefabs.ToArray().ToList();
//
//                 tempPrefabs.Reverse();
//
//                 foreach (var tempPrefab in tempPrefabs)
//                 {
//                     int count = 0;
//
//                     foreach (var prefab in allGameItemPrefabs)
//                     {
//                         if (prefab.id == tempPrefab.id)
//                         {
//                             count++;
//                         }
//                     }
//
//                     if (count >= 2)
//                     {
//                         allGameItemPrefabs.Remove(tempPrefab);
//                     }
//                 }
//             }
//
//             #region Debugging Mode Tools
//
//             [Button(@"@""打开所有"" + " + nameof(prefabFullName) + @"+""的调试模式"""),
//              TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
//             private void EnableAllPrefabsDebuggingMode()
//             {
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.isDebugging = true;
//                 }
//             }
//
//             [Button(@"@""关闭所有"" + " + nameof(prefabFullName) + @"+""的调试模式"""),
//              TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
//             private void DisableAllPrefabsDebuggingMode()
//             {
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.isDebugging = false;
//                 }
//             }
//
//             #endregion
//
//             #region Active State Tools
//
//             [Button(@"@""启用所有的"" + " + nameof(prefabName) + "+" +
//                     nameof(prefabSuffixName)),
//              TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
//             protected void EnableAllPrefabsActiveState()
//             {
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.isActive = true;
//                 }
//             }
//
//             [Button(@"@""禁用所有的"" + " + nameof(prefabName) + "+" +
//                     nameof(prefabSuffixName)),
//              TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
//             protected void DisableAllPrefabsActiveState()
//             {
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.isActive = false;
//                 }
//             }
//
//             #endregion
//
//             #endregion
//
//             #region Game Item Prefab Select
//
//             protected TPrefab selectedPrefab { get; private set; } = null;
//             protected int selectedPrefabIndex { get; private set; } = -1;
//
//             private void AllGameItemPrefabsListSelectHandle(int index)
//             {
//                 if (index < allGameItemPrefabs.Count && index >= 0)
//                 {
//                     OnAllGameItemPrefabsListSelectGUI(index,
//                         allGameItemPrefabs[index]);
//                 }
//             }
//
//             protected virtual void OnAllGameItemPrefabsListSelectGUI(int index,
//                 TPrefab selectedPrefab)
//             {
//                 selectedPrefabIndex = index;
//                 this.selectedPrefab = selectedPrefab;
//             }
//
//             #endregion
//
//             #region Change Type
//
// #if UNITY_EDITOR
//             protected void ChangeType()
//             {
//                 var targetTypes = GetNonIDRegisteredDerivedPrefabType();
//
//                 new TypeSelector(targetTypes, targetType =>
//                 {
//                     allGameItemPrefabs[selectedPrefabIndex] =
//                         selectedPrefab.ConvertToChildOrParent(targetType);
//                     allGameItemPrefabs[selectedPrefabIndex].OnTypeChanged();
//                 }).ShowInPopup();
//             }
// #endif
//
//             #endregion
//
//             #region Query
//
//             [Button(@"@""查询没有预制体扩展的"" + " + nameof(prefabFullName)),
//              TabGroup(TAB_GROUP_NAME, QUERY_PREFABS_CATEGORY, SdfIconType.Tools,
//                  TextColor = "orange")]
//             private void QueryPrefabsWithoutPrefabExtensionGUI()
//             {
//                 GetPrefabsWithoutPrefabExtension().OpenDataFromButton();
//             }
//
//             [Button(@"@""查询有预制体扩展的"" + " + nameof(prefabFullName)),
//              TabGroup(TAB_GROUP_NAME, QUERY_PREFABS_CATEGORY)]
//             private void QueryPrefabsWithPrefabExtensionGUI()
//             {
//                 GetPrefabsWithPrefabExtension().OpenDataFromButton();
//             }
//
//             [Button(@"@""根据种类查询"" + " + nameof(prefabFullName)),
//              TabGroup(TAB_GROUP_NAME, QUERY_PREFABS_CATEGORY)]
//             [HideIf(nameof(gameTypeAbandoned))]
//             public void QueryPrefabsByGameTypeGUI(
//                 [ValueDropdown(nameof(GetAllGameTypeNameList))]
//                 string typeID)
//             {
//                 GetPrefabsByGameType(typeID).OpenDataFromButton();
//             }
//
//             [Button(@"@""查询没有种类的"" + " + nameof(prefabFullName)),
//              TabGroup(TAB_GROUP_NAME, QUERY_PREFABS_CATEGORY)]
//             [HideIf(nameof(gameTypeAbandoned))]
//             private void QueryPrefabsExcludingGameTypeGUI(
//                 [ValueDropdown(nameof(GetAllGameTypeNameList))]
//                 string typeID)
//             {
//                 GetPrefabsExcludingGameType(typeID).OpenDataFromButton();
//             }
//
//             [Button(@"@""根据类型查询"" + " + nameof(prefabFullName)),
//              TabGroup(TAB_GROUP_NAME, QUERY_PREFABS_CATEGORY)]
//             private void QueryPrefabsByTypeGUI(
//                 [ValueDropdown(nameof(GetNonIDRegisteredDerivedPrefabClassNameList))]
//                 Type type)
//             {
//                 GetPrefabsByType(type).OpenDataFromButton();
//             }
//
//             #endregion
//
//             #region Import From Selection
//
//             [Button(@"@""依选中目标添加"" + " + nameof(prefabFullName))]
//             [ShowIf(nameof(_ShowRegisterGameItemPrefabFromSelectionButton))]
//             private void RegisterGameItemPrefabFromSelection()
//             {
// #if UNITY_EDITOR
//                 foreach (var obj in Selection.objects.GetAllAssetsRecursively())
//                 {
//                     OnHandleRegisterGameItemPrefabFromSelectedObject(obj, false);
//                 }
// #endif
//             }
//
//             [Button(@"@""依选中目标添加"" + " + nameof(prefabFullName) + @" + ""（不重复）""")]
//             [ShowIf(nameof(_ShowRegisterGameItemPrefabFromSelectionButton))]
//             private void RegisterUniqueGameItemPrefabFromSelection()
//             {
// #if UNITY_EDITOR
//                 foreach (var obj in Selection.objects.GetAllAssetsRecursively())
//                 {
//                     OnHandleRegisterGameItemPrefabFromSelectedObject(obj, true);
//                 }
// #endif
//             }
//
//             [Button(@"@""依选中目标删除"" + " + nameof(prefabFullName))]
//             [ShowIf(nameof(_ShowRegisterGameItemPrefabFromSelectionButton))]
//             private void UnregisterGameItemPrefabFromSelection()
//             {
// #if UNITY_EDITOR
//                 foreach (var obj in Selection.objects.GetAllAssetsRecursively())
//                 {
//                     OnHandleUnregisterGameItemPrefabFromSelectedObject(obj);
//                 }
// #endif
//             }
//
//             protected virtual void OnHandleRegisterGameItemPrefabFromSelectedObject(
//                 Object obj, bool checkUnique)
//             {
//
//             }
//
//             protected virtual void
//                 OnHandleUnregisterGameItemPrefabFromSelectedObject(
//                     Object obj)
//             {
//
//             }
//
//             private bool _ShowRegisterGameItemPrefabFromSelectionButton()
//             {
// #if UNITY_EDITOR
//                 return ShowRegisterGameItemPrefabFromSelectionButton(Selection
//                     .objects.GetAllAssetsRecursively());
// #else
//                return false;
// #endif
//             }
//
//             protected virtual bool ShowRegisterGameItemPrefabFromSelectionButton(
//                 IEnumerable<Object> objects)
//             {
//                 return false;
//             }
//
//             #endregion
//
//             #region Game Editor Menu Tree Node Provider
//
//             /// <summary>
//             /// 是否在游戏编辑器的左侧罗列预制体
//             /// </summary>
//             [LabelText(@"@""是否在左侧显示"" + prefabName + prefabSuffixName"),
//              TabGroup(TAB_GROUP_NAME, MISCELLANEOUS_SETTING_CATEGORY)]
//             public bool willShowPrefabOnWindowSide = false;
//
//             /// <summary>
//             /// 是否自动将预制体堆叠在左侧
//             /// </summary>
//             [LabelText(@"@""是否自动将预制体堆叠在左侧"" + prefabName + prefabSuffixName"),
//              TabGroup(TAB_GROUP_NAME, MISCELLANEOUS_SETTING_CATEGORY)]
//             public bool autoStackPrefabsOnWindowSide = true;
//
//             bool IGameEditorMenuTreeNodesProvider.isMenuTreeNodesVisible =>
//                 willShowPrefabOnWindowSide;
//
//             bool IGameEditorMenuTreeNodesProvider.autoStackMenuTreeNodes =>
//                 autoStackPrefabsOnWindowSide;
//
//             IEnumerable<IGameEditorMenuTreeNode> IGameEditorMenuTreeNodesProvider.
//                 GetAllMenuTreeNodes()
//             {
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     yield return prefab.CreateTempDataContainer(this);
//                 }
//             }
//
//             #endregion
//
//             #endregion
//
//             #region Check & Init
//
//             #region Game Type
//
//             public void CheckGameTypeInfo()
//             {
//                 typeBasicInfo ??= new();
//                 typeBasicInfo.parentID = "";
//
//                 if (typeBasicInfo.id.IsNullOrEmptyAfterTrim())
//                 {
//                     typeBasicInfo.id =
//                         (prefabName.GetLocalizedString("en-US") + " Type").ToSnakeCase();
//                 }
//                 
//                 typeBasicInfo.name ??= new LocalizedStringReference()
//                 {
//                     defaultValue = typeBasicInfo.id.ToPascalCase(" ")
//                 };
//
//                 if (typeBasicInfo.subtypes.Count == 0)
//                 {
//                     var newSubtype = new GameTypeBasicInfo
//                     {
//                         id = ("default " + prefabName.GetLocalizedString("en-US") + " Type")
//                             .ToSnakeCase()
//                     };
//
//                     newSubtype.name = new()
//                     {
//                         defaultValue = newSubtype.id.ToPascalCase(" ")
//                     };
//
//                     typeBasicInfo.subtypes.Add(newSubtype);
//                 }
//
//                 foreach (var gameTypeInfo in typeBasicInfo.subtypes)
//                 {
//                     if (gameTypeInfo.id.IsNullOrEmptyAfterTrim())
//                     {
//                         continue;
//                     }
//
//                     gameTypeInfo.name ??= new LocalizedStringReference()
//                     {
//                         defaultValue = gameTypeInfo.id.ToPascalCase(" ")
//                     };
//                 }
//
//                 if (defaultTypeID.IsNullOrEmpty())
//                 {
//                     defaultTypeID = GetGameTypeIDList().FirstOrDefault();
//                 }
//             }
//
//             #endregion
//
//             public override void CheckSettings()
//             {
//                 base.CheckSettings();
//
//                 foreach (var derivedPrefabType in GetAllDerivedPrefabType())
//                 {
//                     if (derivedPrefabType.HasAttribute<SerializableAttribute>(false))
//                     {
//                         Debug.LogWarning(
//                             $"{derivedPrefabType}不能有" +
//                             $"{typeof(SerializableAttribute)}的Attribute");
//                     }
//                 }
//
//                 allGameItemPrefabs ??= new();
//
//                 if (ContainsNullPrefab())
//                 {
//                     Note.note.Error($"存在为Null的{prefabName}{prefabSuffixName}");
//                 }
//
//                 if (ContainsEmptyID())
//                 {
//                     Note.note.Error($"存在为空的{prefabName}{prefabSuffixName}ID");
//                 }
//
//                 if (ContainsSameID())
//                 {
//                     Note.note.Error($"存在重复的{prefabName}{prefabSuffixName}ID");
//                 }
//
//                 if (allGameItemPrefabs.Count < minPrefabCount)
//                 {
//                     Debug.LogWarning(
//                         $"至少添加{minPrefabCount}个{prefabName}{prefabSuffixName}");
//                 }
//             }
//
//             protected override void OnPreInit()
//             {
//                 base.OnPreInit();
//
//                 //Init Type Info
//                 if (gameTypeAbandoned == false)
//                 {
//                     CheckGameTypeInfo();
//
//                     if (typeBasicInfo == null ||
//                         typeBasicInfo.id.IsNullOrEmptyAfterTrim())
//                     {
//                         Note.note.Error($"{GetType()}的type为Null或者type.id为空");
//                         return;
//                     }
//
//                     GameType.CreateSubroot(typeBasicInfo.id, typeBasicInfo.name);
//
//                     foreach (var node in typeBasicInfo.PreorderTraverse(true))
//                     {
//                         node.subtypes.Examine(subtype => subtype.parentID = node.id);
//                     }
//
//                     foreach (var node in typeBasicInfo.PreorderTraverse(false))
//                     {
//                         if (node.id.IsNullOrEmptyAfterTrim())
//                         {
//                             Note.note.Error($"{GetType()}存在type的子节点的ID为空");
//                         }
//
//                         GameType.Create(node.id, node.name, node.parentID);
//                     }
//                 }
//
//                 GameItemPrefab.InitStatic();
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.PreInit();
//                 }
//             }
//
//             protected override void OnInit()
//             {
//                 base.OnInit();
//
//                 //Init Prefabs
//                 //int itemSettingCount = allGameItemPrefabs.Count;
//
//                 //if (itemSettingCount == 0)
//                 //{
//                 //    Debug.LogWarning(
//                 //        $"没有任何需要加载的{prefabName}{prefabSuffixName}，请检查{prefabName}通用配置文件");
//                 //}
//                 //else
//                 //{
//                 //    Debug.Log(
//                 //        $"准备加载{itemSettingCount}个{prefabName}{prefabSuffixName}");
//                 //}
//
//                 GameItemPrefab.SetStaticGeneralSetting(this as TGeneralSetting);
//
//                 //int skipCardSettingCount = 0;
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.Init();
//
//                     //try
//                     //{
//
//                     //}
//                     //catch (Exception e)
//                     //{
//                     //    Debug.LogWarning(e.ToString());
//                     //    skipCardSettingCount++;
//                     //}
//                 }
//
//                 //if (skipCardSettingCount <= 0)
//                 //{
//                 //    if (itemSettingCount > 0)
//                 //    {
//                 //        Debug.Log($"{prefabName}{prefabSuffixName}全部加载");
//                 //    }
//                 //}
//                 //else
//                 //{
//                 //    Debug.LogWarning(
//                 //        $"跳过了{skipCardSettingCount}个{prefabName}{prefabSuffixName}的加载");
//                 //}
//             }
//
//             protected override void OnPostInit()
//             {
//                 base.OnPostInit();
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     prefab.PostInit();
//                 }
//             }
//
//             #endregion
//
//             #region Excel
//
//             //private struct TableCellData
//             //{
//             //    public object value;
//             //    public string valueString;
//             //}
//             [TitleGroup("Excel", Order = 400)]
//             [Button("写入Excel文件")]
//             private void WriteToExcel()
//             {
//                 if (dataFolderRelativePath.IsNullOrEmptyAfterTrim())
//                 {
//                     Note.note.Error($"相对路径为空，无法写入Excel文件");
//                 }
//
//                 dataFolderPath.CreateDirectory();
//
//                 var columnNames = new List<string>();
//                 var rows =
//                     new List<Dictionary<string, (object value, string valueString
//                         )>>();
//
//                 JsonSerializer serializer = new();
//
//                 serializer.Converters.AddRange(CustomJSONConverter.converters);
//                 serializer.Converters.AddRange(CustomJSONConverter.converters);
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     var o = JObject.FromObject(prefab, serializer);
//
//                     foreach (var (propertyName, jToken) in o)
//                     {
//                         if (columnNames.Contains(propertyName) == false)
//                         {
//                             columnNames.Add(propertyName);
//                         }
//                     }
//                 }
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     var o = JObject.FromObject(prefab, serializer);
//
//                     var row =
//                         new Dictionary<string, (object value, string valueString)>();
//
//                     foreach (var (propertyName, jToken) in o)
//                     {
//                         row[propertyName] = (
//                             prefab.GetFieldValueByName<object>(propertyName),
//                             jToken.ToString());
//                     }
//
//                     rows.Add(row);
//                 }
//
//                 try
//                 {
//                     using var stream = new FileStream(excelFilePath, FileMode.Create,
//                         FileAccess.Write);
//                     IWorkbook workbook = new XSSFWorkbook();
//                     var excelSheet = (XSSFSheet)workbook.CreateSheet("Sheet1");
//
//                     ExcelCellStyle.HandleWorkbook(workbook, excelSheet);
//
//                     excelSheet.HorizontallyCenter = true;
//                     excelSheet.VerticallyCenter = true;
//                     excelSheet.Autobreaks = true;
//
//                     IRow excelRow = excelSheet.CreateRow(0);
//                     int columnIndex = 0;
//
//                     foreach (string columnName in columnNames)
//                     {
//                         var newCell = excelRow.CreateCell(columnIndex);
//                         newCell.SetCellValue(columnName);
//                         newCell.CellStyle = ExcelCellStyle.GetTitleCellStyle();
//                         columnIndex++;
//                     }
//
//                     int rowIndex = 1;
//
//                     foreach (var row in rows)
//                     {
//                         excelRow = excelSheet.CreateRow(rowIndex);
//
//                         int rowMaxLine = 0;
//                         foreach (var columnName in columnNames)
//                         {
//                             if (row.TryGetValue(columnName, out var cellData))
//                             {
//                                 var columnValueString = cellData.valueString;
//                                 rowMaxLine =
//                                     rowMaxLine.Max(columnValueString.GetLineCount());
//                             }
//                         }
//
//                         excelRow.Height = (short)(rowMaxLine * 300);
//
//                         int cellIndex = 0;
//                         foreach (var columnName in columnNames)
//                         {
//                             if (row.TryGetValue(columnName, out var cellData))
//                             {
//                                 var newCell = excelRow.CreateCell(cellIndex);
//                                 newCell.SetCellValue(cellData.valueString);
//                                 newCell.CellStyle =
//                                     ExcelCellStyle.GetValueCellStyle(cellData.value);
//                             }
//
//                             cellIndex++;
//                         }
//
//                         rowIndex++;
//                     }
//
//                     for (int i = 0; i < columnNames.Count; i++)
//                     {
//                         excelSheet.AutoSizeColumn(i);
//                     }
//
//                     workbook.Write(stream);
//                 }
//                 catch (IOException ioException)
//                 {
//                     Debug.LogError("确保Excel已经关闭");
//                     Debug.LogError(ioException);
//                 }
//             }
//
//             [TitleGroup("Excel", Order = 400)]
//             [Button("读取Excel文件")]
//             private void ReadFromExcel()
//             {
//                 if (dataFolderRelativePath.IsNullOrEmptyAfterTrim())
//                 {
//                     Note.note.Error($"相对路径为空，无法读取Excel文件");
//                 }
//
//                 if (excelFilePath.ExistsFile() == false)
//                 {
//                     Note.note.Error("不存在指定的Excel文件，请先创建Excel文件或写入Excel文件");
//                 }
//
//                 try
//                 {
//                     using var stream = new FileStream(excelFilePath, FileMode.Open);
//
//                     stream.Position = 0;
//
//                     XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
//                     var sheet = (XSSFSheet)xssWorkbook.GetSheetAt(0);
//
//                     IRow headerRow = sheet.GetRow(0);
//
//                     int totalColumnCount = headerRow.LastCellNum;
//
//                     var validColumnIndices = new HashSet<int>();
//                     var validPropertyNames = new Dictionary<int, string>();
//
//                     for (int columnIndex = 0;
//                          columnIndex < totalColumnCount;
//                          columnIndex++)
//                     {
//                         ICell cell = headerRow.GetCell(columnIndex);
//
//                         if (cell == null || cell.ToString().IsNullOrEmptyAfterTrim())
//                         {
//                             Debug.LogError($"第{columnIndex + 1}列的标题无效，为空");
//                             continue;
//                         }
//
//                         var propertyName = cell.ToString().Trim();
//
//                         if (columnIndex == 0)
//                         {
//                             if (propertyName != "id")
//                             {
//                                 Debug.LogError($"第1列的标题必须是id");
//                                 return;
//                             }
//                         }
//                         else
//                         {
//                             validColumnIndices.Add(columnIndex);
//                             validPropertyNames[columnIndex] = propertyName;
//                         }
//                     }
//
//                     for (int rowIndex = (sheet.FirstRowNum + 1);
//                          rowIndex <= sheet.LastRowNum;
//                          rowIndex++)
//                     {
//                         IRow row = sheet.GetRow(rowIndex);
//
//                         if (row == null)
//                         {
//                             continue;
//                         }
//
//                         var firstCell = row.GetCell(0);
//
//                         if (firstCell.CellType == CellType.Blank)
//                         {
//                             Debug.LogError($"第{rowIndex + 1}行的第一个单元格为空，此处应为ID");
//                             continue;
//                         }
//
//                         var id = firstCell.ToString();
//
//                         if (id.IsNullOrEmptyAfterTrim())
//                         {
//                             Debug.LogError($"第{rowIndex + 1}行的ID为空，无效");
//                             continue;
//                         }
//
//                         var prefab = GetPrefab(id);
//
//                         if (prefab == null)
//                         {
//                             AddPrefab(id);
//                             prefab = GetPrefab(id);
//
//                             Debug.LogWarning($"新添了id为{id}的预制体");
//                         }
//
//                         foreach (var validColumnIndex in validColumnIndices)
//                         {
//                             var propertyName = validPropertyNames[validColumnIndex];
//
//                             var cell = row.GetCell(validColumnIndex);
//
//                             if (cell == null || cell.ToString().IsNullOrEmpty())
//                             {
//                                 continue;
//                             }
//
//                             var fieldInfo = prefab.GetType()
//                                 .GetFieldByName(propertyName);
//
//                             if (fieldInfo == null)
//                             {
//                                 Debug.LogError(
//                                     $"第{rowIndex + 1}行，第{validColumnIndex + 1}列：" +
//                                     $"{prefab.GetType()}不存在名为{propertyName}的字段");
//                                 continue;
//                             }
//
//                             var propertyValueJSON = cell.ToString();
//
//                             try
//                             {
//                                 if (fieldInfo.FieldType == typeof(string))
//                                 {
//                                     fieldInfo.SetValue(prefab, propertyValueJSON);
//                                 }
//                                 else if (fieldInfo.FieldType == typeof(bool))
//                                 {
//                                     propertyValueJSON = propertyValueJSON
//                                         .Trim('\"', '\'', ' ', '\n', '\r').ToUpper();
//                                     if (propertyValueJSON == "TRUE")
//                                     {
//                                         fieldInfo.SetValue(prefab, true);
//                                     }
//                                     else if (propertyValueJSON == "FALSE")
//                                     {
//                                         fieldInfo.SetValue(prefab, false);
//                                     }
//                                     else
//                                     {
//                                         throw new JsonReaderException(
//                                             $"{propertyValueJSON} 不是True也不是False");
//                                     }
//                                 }
//                                 else
//                                 {
//                                     if (fieldInfo.FieldType.IsEnum)
//                                     {
//                                         propertyValueJSON =
//                                             "\"" + propertyValueJSON + "\"";
//                                     }
//
//                                     var propertyValue =
//                                         JsonConvert.DeserializeObject(
//                                             propertyValueJSON,
//                                             fieldInfo.FieldType,
//                                             CustomJSONConverter.converters);
//
//                                     fieldInfo.SetValue(prefab, propertyValue);
//                                 }
//                             }
//                             catch (JsonReaderException jsonReaderException)
//                             {
//                                 Debug.LogError(
//                                     $"在读取第{rowIndex + 1}行，第{validColumnIndex + 1}列，发生JSON解序列化错误，请注意格式");
//                                 Debug.LogError(jsonReaderException);
//                             }
//                         }
//                     }
//                 }
//                 catch (IOException ioException)
//                 {
//                     Debug.LogError("确保Excel已经关闭");
//                     Debug.LogError(ioException);
//                 }
//             }
//
//             [TitleGroup("Excel", Order = 400)]
//             [Button("打开Excel文件")]
//             public void OpenExcel()
//             {
//                 excelFilePath.OpenFile();
//             }
//
//             #endregion
//
//             #region Prefab
//
//             #region Remov ePrefab
//
//             protected void RemovePrefab(string prefabID)
//             {
//                 allGameItemPrefabs.RemoveAll(prefab => prefab.id == prefabID);
//             }
//
//             protected void RemovePrefab(TPrefab prefab)
//             {
//                 allGameItemPrefabs.Remove(prefab);
//             }
//
//             protected void RemovePrefab(Predicate<TPrefab> condition)
//             {
//                 allGameItemPrefabs.RemoveAll(condition);
//             }
//
//             #endregion
//
//             #region Add Prefab
//
//             /// <summary>
//             /// 添加预制体，当预制体列表不存在参数newPrefabID对应的ID时，才会添加新的ID为newPrefabID的预制体
//             /// 仅用于Editor模式
//             /// </summary>
//             /// <param name="newPrefabID"></param>
//             public void AddPrefab(string newPrefabID)
//             {
//                 var newPrefab = (TPrefab)typeof(TPrefab).CreateInstance();
//                 newPrefab.id = newPrefabID;
//
//                 AddPrefab(newPrefab);
//             }
//
//             /// <summary>
//             /// 添加预制体，当预制体列表不存在参数prefab的ID时，才会添加此prefab
//             /// 仅用于Editor模式
//             /// </summary>
//             /// <param name="prefab"></param>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public void AddPrefab([NotNull] TPrefab prefab)
//             {
//                 if (prefab.id.IsNullOrEmptyAfterTrim())
//                 {
//                     Debug.LogWarning("id为空");
//                 }
//
//                 if (ContainsPrefabID(prefab.id) == false)
//                 {
//                     allGameItemPrefabs.Add(prefab);
//                 }
//             }
//
//             /// <summary>
//             /// 添加预制体到列表第一个，当预制体列表不存在参数prefab的ID时，才会添加此prefab
//             /// 仅用于Editor模式
//             /// </summary>
//             /// <param name="prefab"></param>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public void AddPrefabToFirst([NotNull] TPrefab prefab)
//             {
//                 if (prefab.id.IsNullOrEmptyAfterTrim())
//                 {
//                     Debug.LogWarning("id为空");
//                 }
//
//                 if (ContainsPrefabID(prefab.id) == false)
//                 {
//                     allGameItemPrefabs.Insert(0, prefab);
//                 }
//             }
//
//             /// <summary>
//             /// 添加预制体，当预制体列表不存在参数prefab的ID时，才会添加此prefab
//             /// 与AddPrefab不同的是，当prefab的ID为空或者已经存在prefab.id的时候，会报错而不是发出警告
//             /// 仅用于Editor模式
//             /// </summary>
//             /// <param name="prefab"></param>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public void AddPrefabStrictly([NotNull] TPrefab prefab)
//             {
//                 if (prefab.id.IsNullOrEmptyAfterTrim())
//                 {
//                     Note.note.Error("id为空");
//                 }
//
//                 if (ContainsPrefabID(prefab.id) == false)
//                 {
//                     allGameItemPrefabs.Add(prefab);
//                 }
//                 else
//                 {
//                     Note.note.Error($"添加id为{prefab.id}的{prefabName}失败，已存在此id");
//                 }
//             }
//
//             /// <summary>
//             /// 尝试添加往预制体列表里添加新的prefab，当id为空或者id已经存在时，会返回False，否则返回True
//             /// </summary>
//             /// <param name="prefab"></param>
//             /// <param name="newPrefab"></param>
//             /// <returns></returns>
//             public bool TryAddPrefab(TPrefab prefab, out TPrefab newPrefab)
//             {
//                 if (prefab.id.IsNullOrEmptyAfterTrim())
//                 {
//                     Debug.LogWarning("id为空");
//                     newPrefab = null;
//                     return false;
//                 }
//
//                 if (ContainsPrefabID(prefab.id) == false)
//                 {
//                     allGameItemPrefabs.Add(prefab);
//                     newPrefab = prefab;
//                     return true;
//                 }
//
//                 newPrefab = null;
//                 return false;
//             }
//
//             /// <summary>
//             /// 尝试添加往预制体列表里添加新的prefab，当newPrefabID为空或者newPrefabID已经存在时，会返回False，否则返回True
//             /// </summary>
//             /// <param name="newPrefabID"></param>
//             /// <param name="newPrefab"></param>
//             /// <returns></returns>
//             public bool TryAddPrefab(string newPrefabID, out TPrefab newPrefab)
//             {
//                 var newPrefabTemp = (TPrefab)typeof(TPrefab).CreateInstance();
//                 newPrefabTemp.id = newPrefabID;
//
//                 return TryAddPrefab(newPrefabTemp, out newPrefab);
//             }
//
//             #endregion
//
//             #region Get Prefab
//
//             #region Get All Prefabs
//
//             /// <summary>
//             /// 获取所有预制体
//             /// </summary>
//             /// <returns>返回包含所有预制体的List</returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public IEnumerable<TPrefab> GetAllPrefabs()
//             {
//                 return allGameItemPrefabs;
//             }
//
//             public IEnumerable<TPrefab> GetAllActivePrefabs()
//             {
//                 return allGameItemPrefabs.Where(prefab => prefab.isActive);
//             }
//
//             #endregion
//
//             #region Get All Prefabs ID
//
//             /// <summary>
//             /// 获取全部预制体的ID
//             /// </summary>
//             /// <returns>返回所有预制体的ID构成的IEnumerable</returns>
//             public IEnumerable<string> GetAllPrefabsID()
//             {
//                 return allGameItemPrefabs?.Select(prefab => prefab.id);
//             }
//
//             /// <summary>
//             /// 获取全部isActive为True的预制体的ID
//             /// </summary>
//             /// <returns></returns>
//             public IEnumerable<string> GetAllActivePrefabsID()
//             {
//                 return GetAllActivePrefabs().Select(prefab => prefab.id);
//             }
//
//             #endregion
//
//             #region Get Prefab By ID
//
//             /// <summary>
//             /// 通过ID获取预制体，如果没有会返回Null
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public TPrefab GetPrefab(string id)
//             {
//                 if (initDone && Application.isPlaying)
//                 {
//                     return GameItemPrefab.GetPrefab(id);
//                 }
//
//                 if (allGameItemPrefabs == null || allGameItemPrefabs.Count == 0)
//                 {
//                     return null;
//                 }
//
//                 return allGameItemPrefabs.FirstOrDefault(prefab =>
//                     prefab != null && prefab.id == id);
//             }
//
//             /// <summary>
//             /// 通过ID获取isActive为True的预制体，如果没有会返回Null
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public TPrefab GetActivePrefab(string id)
//             {
//                 if (initDone && Application.isPlaying)
//                 {
//                     return GameItemPrefab.GetActivePrefab(id);
//                 }
//
//                 if (allGameItemPrefabs == null || allGameItemPrefabs.Count == 0)
//                 {
//                     return null;
//                 }
//
//                 return allGameItemPrefabs.FirstOrDefault(prefab =>
//                     prefab != null && prefab.id == id && prefab.isActive);
//             }
//
//             /// <summary>
//             /// 严格通过ID获取预制体，如果没有就报错
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public TPrefab GetPrefabStrictly(string id)
//             {
//                 var prefab = GetPrefab(id);
//
//                 if (prefab == null)
//                 {
//                     Note.note.Error($"id为{id}的{prefabName}{prefabSuffixName}不存在");
//                 }
//
//                 return prefab;
//             }
//
//             #endregion
//
//             #region Get Prefab By Game Type
//
//             public IReadOnlyList<TPrefab> GetPrefabsByGameType(string gameTypeID)
//             {
//                 if (gameTypeAbandoned)
//                 {
//                     return null;
//                 }
//
//                 if (typeBasicInfo.TryFindChild(false,
//                         typeInfo => typeInfo.id == gameTypeID, out var typeInfo) ==
//                     false)
//                 {
//                     Debug.LogWarning($"不存在ID为{gameTypeID}的{prefabName}种类");
//                     return null;
//                 }
//
//                 if (allGameItemPrefabs == null)
//                 {
//                     return null;
//                 }
//
//                 var results = new List<TPrefab>();
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     if (prefab == null)
//                     {
//                         continue;
//                     }
//
//                     foreach (var prefabGameTypeID in prefab.initialGameTypesID)
//                     {
//                         if (typeInfo.HasChild(true,
//                                 typeInfo => typeInfo.id == prefabGameTypeID))
//                         {
//                             results.Add(prefab);
//                             break;
//                         }
//                     }
//                 }
//
//                 return results;
//             }
//
//             public IReadOnlyList<TPrefab> GetPrefabsExcludingGameType(
//                 string gameTypeID)
//             {
//                 if (gameTypeAbandoned)
//                 {
//                     return null;
//                 }
//
//                 if (typeBasicInfo.TryFindChild(false,
//                         typeInfo => typeInfo.id == gameTypeID, out var typeInfo) ==
//                     false)
//                 {
//                     Debug.LogWarning($"不存在ID为{gameTypeID}的{prefabName}种类");
//                     return null;
//                 }
//
//                 if (allGameItemPrefabs == null)
//                 {
//                     return null;
//                 }
//
//                 var results = new List<TPrefab>();
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     if (prefab is { isActive: true })
//                     {
//                         foreach (var prefabGameTypeID in prefab.initialGameTypesID)
//                         {
//                             if (typeInfo.HasChild(true,
//                                     typeInfo => typeInfo.id == prefabGameTypeID) ==
//                                 false)
//                             {
//                                 results.Add(prefab);
//                                 break;
//                             }
//                         }
//                     }
//                 }
//
//                 return results;
//             }
//
//             #endregion
//
//             #region Get Prefab By Type
//
//             public IReadOnlyList<TPrefab> GetPrefabsByType(Type type)
//             {
//                 if (allGameItemPrefabs == null)
//                 {
//                     return null;
//                 }
//
//                 var results = new List<TPrefab>();
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     if (prefab is { isActive: true } &&
//                         type.IsInstanceOfType(prefab))
//                     {
//                         results.Add(prefab);
//                     }
//                 }
//
//                 return results;
//             }
//
//             #endregion
//
//             #region Get Prefab Name
//
//             /// <summary>
//             /// 获取特定ID预制体的名称
//             /// </summary>
//             /// <param name="id">预制体ID</param>
//             /// <returns></returns>
//             public string GetPrefabName(string id)
//             {
//                 var result = GetPrefab(id);
//
//                 if (result != null)
//                 {
//                     return result.name;
//                 }
//
//                 return $"{id}不存在，获取名称失败";
//             }
//
//             #endregion
//
//             #region Get Random Prefab
//
//             /// <summary>
//             /// 随机获取一个预制体
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public TPrefab GetRandomPrefab()
//             {
//                 if (initDone && Application.isPlaying)
//                 {
//                     return GameItemPrefab.GetRandomPrefab();
//                 }
//
//                 return allGameItemPrefabs.Where(prefab => prefab is not null)
//                     .Choose();
//             }
//
//             /// <summary>
//             /// 随机获得一个isActive为True的预制体
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public TPrefab GetRandomActivePrefab()
//             {
//                 return allGameItemPrefabs
//                     .Where(prefab => prefab is { isActive: true })
//                     .Choose();
//             }
//
//             /// <summary>
//             /// 随机获取预制体的ID，如果无预制体返回NUll
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public string GetRandomPrefabID()
//             {
//                 return GetRandomPrefab()?.id;
//             }
//
//             /// <summary>
//             /// 随机获取isActive为True的预制体ID，如果无预制体返回NUll
//             /// </summary>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public string GetRandomActivePrefabID()
//             {
//                 return GetRandomActivePrefab()?.id;
//             }
//
//             #endregion
//
//             #region Get Prefabs Without Extension
//
//             public IEnumerable<TPrefab> GetPrefabsWithoutPrefabExtension()
//             {
//                 return allGameItemPrefabs.Where(prefab =>
//                     prefab is not null && prefab.extendedPrefabType is null);
//             }
//
//             public IEnumerable<TPrefab> GetPrefabsWithPrefabExtension()
//             {
//                 return allGameItemPrefabs
//                     .Where(prefab => prefab?.extendedPrefabType != null);
//             }
//
//             #endregion
//
//             #endregion
//
//             #region Contains
//
//             public bool ContainsPrefabWhere(Func<TPrefab, bool> predicate)
//             {
//                 if (allGameItemPrefabs == null || allGameItemPrefabs.Count == 0)
//                 {
//                     return false;
//                 }
//
//                 return allGameItemPrefabs.Any(predicate);
//             }
//
//             /// <summary>
//             /// 预制体列表是否包含某个特定ID
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public bool ContainsPrefabID(string id)
//             {
//                 if (initDone && Application.isPlaying)
//                 {
//                     return GameItemPrefab.ContainsPrefabID(id);
//                 }
//
//                 return ContainsPrefabWhere(prefab =>
//                     prefab != null && prefab.id == id);
//             }
//
//             /// <summary>
//             /// 预制体列表是否包含某个特定ID，且此预制体的isActive为True
//             /// </summary>
//             /// <param name="id"></param>
//             /// <returns></returns>
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public bool ContainsActivePrefabID(string id)
//             {
//                 if (initDone && Application.isPlaying)
//                 {
//                     return GameItemPrefab.ContainsActivePrefabID(id);
//                 }
//
//                 return ContainsPrefabWhere(prefab =>
//                     prefab != null && prefab.id == id &&
//                     prefab.isActive);
//             }
//
//             /// <summary>
//             /// 预制体列表里是否包含重复ID
//             /// </summary>
//             /// <returns></returns>
//             public bool ContainsSameID()
//             {
//                 return allGameItemPrefabs.Where(prefab => prefab != null)
//                     .ContainsSame(prefab => prefab.id);
//             }
//
//             /// <summary>
//             /// 预制体列表里是否包含空ID
//             /// </summary>
//             /// <returns></returns>
//             public bool ContainsEmptyID()
//             {
//                 if (allGameItemPrefabs == null || allGameItemPrefabs.Count == 0)
//                 {
//                     return false;
//                 }
//
//                 return allGameItemPrefabs.Where(prefab => prefab != null)
//                     .Select(prefab => prefab.id)
//                     .Any(StringUtility.IsNullOrEmptyAfterTrim);
//             }
//
//             /// <summary>
//             /// 预制体列表里是否包含Null
//             /// </summary>
//             /// <returns></returns>
//             public bool ContainsNullPrefab()
//             {
//                 return allGameItemPrefabs.ContainsNull();
//             }
//
//             public bool ContainsPrefabByType(Type type)
//             {
//                 return allGameItemPrefabs.Any(prefab => prefab != null &&
//                     type.IsInstanceOfType(prefab));
//             }
//
//             #endregion
//
//             #endregion
//
//             #region Game Type
//
//             public string GetGameTypeName(string gameTypeID)
//             {
//                 if (rootGameType == null)
//                 {
//                     return typeBasicInfo.PreorderTraverse(false)
//                         .FirstOrDefault(node => node.id == gameTypeID)?.name;
//                 }
//
//                 return GameType.GetGameType(gameTypeID)?.name;
//             }
//
//             /// <summary>
//             /// 获取所有叶节点种类的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//             /// </summary>
//             /// <returns></returns>
//             public IEnumerable<ValueDropdownItem<string>> GetGameTypeNameList()
//             {
//                 return typeBasicInfo.GetAllLeaves(false)
//                     .Select(subType =>
//                         new ValueDropdownItem<string>(subType.name, subType.id));
//             }
//
//             /// <summary>
//             /// 获取所有叶节点种类的ID列表
//             /// </summary>
//             /// <returns></returns>
//             public IEnumerable<string> GetGameTypeIDList()
//             {
//                 return typeBasicInfo.GetAllLeaves(false)
//                     .Select(subType => subType.id);
//             }
//
//             /// <summary>
//             /// 获取所有种类的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//             /// </summary>
//             /// <returns></returns>
//             public IEnumerable<ValueDropdownItem<string>> GetAllGameTypeNameList()
//             {
//                 return typeBasicInfo.PreorderTraverse(false)
//                     .Select(subType =>
//                         new ValueDropdownItem<string>(subType.name, subType.id));
//             }
//
//             /// <summary>
//             /// 获取所有种类的ID列表
//             /// </summary>
//             /// <returns></returns>
//             public IEnumerable<string> GetAllGameTypeIDList()
//             {
//                 return typeBasicInfo.PreorderTraverse(false)
//                     .Select(subType => subType.id);
//             }
//
//             /// <summary>
//             /// 随机获取一个种类的ID
//             /// </summary>
//             /// <returns></returns>
//             public string GetRandomGameTypeID()
//             {
//                 return GetAllGameTypeIDList().Choose();
//             }
//
//             /// <summary>
//             /// 是否包含此种类
//             /// </summary>
//             /// <param name="gameTypeID"></param>
//             /// <returns></returns>
//             public bool ContainsGameType(string gameTypeID)
//             {
//                 return typeBasicInfo.GetAllLeaves(false)
//                     .Any(node => node.id == gameTypeID);
//             }
//
//             private GameTypeBasicInfo GetGameTypeBasicInfo(string gameTypeID)
//             {
//                 return typeBasicInfo.PreorderTraverse(false)
//                     .FirstOrDefault(node => node.id == gameTypeID);
//             }
//
//             public IEnumerable<ValueDropdownItem<string>>
//                 GetPrefabNameListByGameType(
//                     string gameTypeID)
//             {
//                 foreach (var prefab in GetPrefabsByGameType(gameTypeID))
//                 {
//                     yield return new ValueDropdownItem<string>(prefab.name,
//                         prefab.id);
//                 }
//             }
//
//             #endregion
//
//             #region Get Name List
//
//             #region Get Prefab Name List
//
//             /// <summary>
//             /// 获取所有预制体的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//             /// 比如 [ValueDropdown("@GameSetting.inputSystemGeneralSetting.GetPrefabNameList()")]
//             /// </summary>
//             /// <returns>返回为ValueDropdownItem构成的IEnumerable</returns>
//             public IEnumerable<ValueDropdownItem<string>> GetPrefabNameList()
//             {
//                 if (allGameItemPrefabs == null)
//                 {
//                     yield break;
//                 }
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     if (prefab is not null)
//                     {
//                         yield return prefab.GetNameIDDropDownItem();
//                     }
//                 }
//             }
//
//             /// <summary>
//             /// 获取特定Type对应的类或其派生类的预制体的名称和ID，一般用于Odin插件里的ValueDropdown Attribute
//             /// 比如 [ValueDropdown("@GameSetting.uiPanelGeneralSetting.GetPrefabNameList(typeof(ContainerUIPreset))")]
//             /// </summary>
//             /// <param name="specificTypes">约束预制体的类型</param>
//             /// <returns>返回为ValueDropdownItem构成的IEnumerable</returns>
//             public IEnumerable<ValueDropdownItem<string>> GetPrefabNameList(
//                 params Type[] specificTypes)
//             {
//                 if (allGameItemPrefabs == null)
//                 {
//                     yield break;
//                 }
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     if (prefab is not null && specificTypes.Any(specificType =>
//                             specificType.IsInstanceOfType(prefab)))
//                     {
//                         yield return prefab.GetNameIDDropDownItem();
//                     }
//                 }
//             }
//
//             public IEnumerable<ValueDropdownItem<string>> GetPrefabNameList(
//                 Type specificType)
//             {
//                 if (allGameItemPrefabs == null)
//                 {
//                     yield break;
//                 }
//
//                 foreach (var prefab in allGameItemPrefabs)
//                 {
//                     if (prefab is not null && specificType.IsInstanceOfType(prefab))
//                     {
//                         yield return prefab.GetNameIDDropDownItem();
//                     }
//                 }
//             }
//
//             #endregion
//
//             #region Get Derived Prefab Class Name List
//
//             public IEnumerable<ValueDropdownItem<Type>>
//                 GetNonIDRegisteredDerivedPrefabClassNameList()
//             {
//                 foreach (var derivedPrefabType in
//                          GetNonIDRegisteredDerivedPrefabType())
//                 {
//                     yield return new ValueDropdownItem<Type>(
//                         derivedPrefabType.FullName,
//                         derivedPrefabType);
//                 }
//             }
//
//             public IEnumerable<ValueDropdownItem<Type>>
//                 GetAllDerivedPrefabClassNameList()
//             {
//                 foreach (var derivedPrefabType in GetAllDerivedPrefabType())
//                 {
//                     yield return new ValueDropdownItem<Type>(
//                         derivedPrefabType.FullName,
//                         derivedPrefabType);
//                 }
//             }
//
//             #endregion
//
//             #endregion
//
//             #region Get Type
//
//             public IEnumerable<Type> GetAllDerivedPrefabType()
//             {
//                 return typeof(TPrefab).GetDerivedClasses(true, false);
//             }
//
//             public IEnumerable<Type> GetNonIDRegisteredDerivedPrefabType()
//             {
//                 return typeof(TPrefab).GetDerivedClasses(true, false).Where(type =>
//                     type.HasFieldByName(REGISTERED_ID_NAME) == false &&
//                     type.HasPropertyByName(REGISTERED_ID_NAME) == false);
//             }
//
//             #endregion
//         }
//     }
// }
