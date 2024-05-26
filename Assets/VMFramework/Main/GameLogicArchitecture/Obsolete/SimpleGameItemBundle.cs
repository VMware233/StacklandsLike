// using Newtonsoft.Json;
// using Sirenix.OdinInspector;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using Basis.Core;
// using VMFramework.Core;
// using UnityEngine;
// using VMFramework.OdinExtensions;
// using VMFramework.Core.Editor;
//
// #if FISHNET
//
// using FishNet;
// using FishNet.Managing.Client;
// using FishNet.Managing.Server;
// using FishNet.Serializing;
//
// #endif
//
// namespace VMFramework.GameLogicArchitecture
// {
//     public class
//         SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance> :
//             GamePrefabCoreBundle<TPrefab, TGeneralSetting>
//         where TPrefab : SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance>.
//         GameItemPrefab
//         where TGeneralSetting : SimpleGameItemBundle<TPrefab, TGeneralSetting,
//             TInstance>.GameItemGeneralSetting
//         where TInstance : SimpleGameItemBundle<TPrefab, TGeneralSetting, TInstance>.
//         GameItem
//     {
//         public const string BIND_INSTANCE_TYPE_NAME = "bindInstanceType";
//
//         #region Extension Utility
//
//         protected static Type GetBindInstanceType(Type prefabType)
//         {
//             Type bindInstanceType = null;
//
//             var bindInstanceTypeFieldInfo =
//                 prefabType.GetFieldByName(BIND_INSTANCE_TYPE_NAME);
//
//             if (bindInstanceTypeFieldInfo != null)
//             {
//                 if (bindInstanceTypeFieldInfo.FieldType == typeof(Type))
//                 {
//                     if (bindInstanceTypeFieldInfo.IsStatic)
//                     {
//                         bindInstanceType =
//                             bindInstanceTypeFieldInfo.GetValue(null) as Type;
//                     }
//                     else
//                     {
//                         Debug.LogWarning(
//                             $"{prefabType}名为{BIND_INSTANCE_TYPE_NAME}的字段" +
//                             $"需要为Static或Const");
//                     }
//                 }
//                 else
//                 {
//                     Debug.LogWarning(
//                         $"{prefabType}的{BIND_INSTANCE_TYPE_NAME}的" +
//                         $"Type需要为{typeof(Type).FullName}," +
//                         $"而不是{bindInstanceTypeFieldInfo.FieldType}");
//                 }
//             }
//
//             if (bindInstanceType == null)
//             {
//                 var bindInstanceTypePropertyInfo =
//                     prefabType.GetPropertyByName(BIND_INSTANCE_TYPE_NAME);
//
//                 if (bindInstanceTypePropertyInfo != null)
//                 {
//                     if (bindInstanceTypePropertyInfo.PropertyType ==
//                         typeof(Type))
//                     {
//                         if (bindInstanceTypePropertyInfo.IsStatic())
//                         {
//                             bindInstanceType =
//                                 bindInstanceTypePropertyInfo.GetValue(null)
//                                     as Type;
//                         }
//                         else
//                         {
//                             Debug.LogWarning(
//                                 $"{prefabType}名为{BIND_INSTANCE_TYPE_NAME}的Property" +
//                                 $"需要为Static");
//                         }
//                     }
//                     else
//                     {
//                         Debug.LogWarning(
//                             $"{prefabType}的{BIND_INSTANCE_TYPE_NAME}" +
//                             $"的Type需要为{typeof(Type).FullName}," +
//                             $"而不是{bindInstanceTypePropertyInfo.PropertyType}");
//                     }
//                 }
//             }
//
//             return bindInstanceType;
//         }
//
//         private static List<Type> derivedInstanceTypesCache;
//
//         public static IEnumerable<Type> GetDerivedInstanceTypes()
//         {
//             derivedInstanceTypesCache ??= typeof(TInstance).GetDerivedClasses(true,
//                 false).ToList();
//
//             return derivedInstanceTypesCache;
//         }
//
//         private static List<Type> derivedInstanceTypesWithoutRegisteredIDCache;
//
//         public static IEnumerable<Type> GetDerivedInstanceTypesWithoutRegisteredID()
//         {
//             derivedInstanceTypesWithoutRegisteredIDCache ??=
//                 GetDerivedInstanceTypes()
//                     .Where(type => HasRegisteredID(type) == false).ToList();
//
//             return derivedInstanceTypesWithoutRegisteredIDCache;
//         }
//
//         #endregion
//
//         [HideReferenceObjectPicker]
//         [PreviewComposite]
//         [HideInEditorMode]
//         [HideDuplicateReferenceBox]
//         public abstract class GameItem : IIDOwner, IReadOnlyGameTypeOwner, INameOwner
//         {
//             public const string NULL_ID = GamePrefabCoreBundle<TPrefab, TGeneralSetting>.NULL_ID;
//
//             public static bool isInitialized = false;
//
//             [ShowInInspector]
//             public static bool gameTypeAbandoned => GameItemPrefab.gameTypeAbandoned;
//
//             /// <summary>
//             /// 由哪个预制体生成的此实例
//             /// </summary>
//             [LabelText("预制体")]
//             [ShowInInspector]
//             protected TPrefab origin;
//
//             /// <summary>
//             /// 实例的ID，与预制体的ID一一对应
//             /// </summary>
//             [LabelText("ID")]
//             [ShowInInspector, DisplayAsString]
//             public string id { get; private set; }
//
//             [LabelText("游戏种类")]
//             [ShowInInspector]
//             [HideIf(nameof(gameTypeAbandoned))]
//             public IReadOnlyGameTypeSet gameTypeSet => origin.gameTypeSet;
//
//             [LabelText("唯一游戏种类")]
//             [ShowInInspector]
//             public GameType uniqueGameType { get; private set; }
//
//             /// <summary>
//             /// 是否在调试此ID对应的预制体或实例
//             /// </summary>
//             [ShowInInspector]
//             public bool isDebugging => origin.isDebugging;
//
//             public string name => origin.name;
//
//             protected TGeneralSetting generalSetting =>
//                 GameItemPrefab.GetStaticGeneralSetting();
//
//             #region FishNet Properties
//
// #if FISHNET
//
//             public bool isServer
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get => InstanceFinder.IsServerStarted;
//             }
//
//             public bool isClient
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get => InstanceFinder.IsClientStarted;
//             }
//
//             public bool isHost
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get => InstanceFinder.IsHostStarted;
//             }
//
//             public bool isServerOnly
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get => InstanceFinder.IsServerOnlyStarted;
//             }
//
//             public bool isClientOnly
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get => InstanceFinder.IsClientOnlyStarted;
//             }
//
//             public ServerManager serverManager => InstanceFinder.ServerManager;
//             public ClientManager clientManager => InstanceFinder.ClientManager;
//
// #endif
//
//             #endregion
//
//             #region Is Same
//
//             /// <summary>
//             /// 判断两者是否一样
//             /// </summary>
//             /// <param name="other">另一个</param>
//             /// <returns></returns>
//             public virtual bool IsSameAs(TInstance other)
//             {
//
//                 return id == other.id;
//             }
//
//             #endregion
//
//             #region Net Serialization
//
// #if FISHNET
//
//             /// <summary>
//             /// 在网络上如何传输，当在此实例被写进byte流时调用
//             /// </summary>
//             /// <param name="writer"></param>
//             protected virtual void OnWrite(Writer writer)
//             {
//
//             }
//
//             /// <summary>
//             /// 在网络上如何传输，当在此实例被从byte流中读出时调用
//             /// </summary>
//             /// <param name="reader"></param>
//             protected virtual void OnRead(Reader reader)
//             {
//
//             }
//
//             /// <summary>
//             /// Fishnet的网络byte流写入
//             /// </summary>
//             /// <param name="writer"></param>
//             /// <param name="gameItem"></param>
//             public static void Write(Writer writer, GameItem gameItem)
//             {
//                 if (gameItem == null)
//                 {
//                     writer.WriteString(NULL_ID);
//                 }
//                 else
//                 {
//                     writer.WriteString(gameItem.id);
//
//                     gameItem.OnWrite(writer);
//                 }
//             }
//
//             /// <summary>
//             /// Fishnet的网络byte流读出
//             /// </summary>
//             /// <param name="reader"></param>
//             /// <returns></returns>
//             public static TInstance Read(Reader reader)
//             {
//                 var id = reader.ReadString();
//
//                 if (id == NULL_ID)
//                 {
//                     return null;
//                 }
//
//                 var gameItem = Create(id);
//
//                 gameItem.OnRead(reader);
//
//                 return gameItem;
//             }
//
// #endif
//
//             #endregion
//
//             #region Clone
//
//             /// <summary>
//             /// 当被复制时调用
//             /// </summary>
//             /// <param name="other"></param>
//             protected virtual void OnClone(TInstance other)
//             {
//
//             }
//
//             /// <summary>
//             /// 获得复制
//             /// </summary>
//             /// <returns></returns>
//             public TInstance GetClone()
//             {
//                 var clone = Create(id);
//
//                 clone.OnClone(this as TInstance);
//
//                 return clone;
//             }
//
//             #endregion
//
//             #region String
//
//             protected virtual IEnumerable<(string propertyID, string propertyContent)> OnGetStringProperties()
//             {
//                 yield break;
//             }
//
//             public override string ToString()
//             {
//                 var extraString = OnGetStringProperties()
//                     .Select(property => property.propertyID + ":" + property.propertyContent)
//                     .Join(", ");
//
//                 return $"[{GetType()}:id:{id},{extraString}]";
//             }
//
//             #endregion
//
//             #region Origin
//
//             /// <summary>
//             /// 获得此实例的预制体
//             /// </summary>
//             /// <typeparam name="T"></typeparam>
//             /// <returns></returns>
//             protected T GetOrigin<T>() where T : TPrefab, new()
//             {
//                 return origin as T;
//             }
//
//             /// <summary>
//             /// 获得此实例的预制体，如果获取失败则报错
//             /// </summary>
//             /// <typeparam name="T"></typeparam>
//             /// <returns></returns>
//             protected T GetOriginStrictly<T>() where T : TPrefab, new()
//             {
//                 var newOrigin = origin as T;
//
//                 if (newOrigin == null)
//                 {
//                     Note.note.Error($"{this}不是{typeof(T)}");
//                 }
//
//                 return newOrigin;
//             }
//
//             #endregion
//
//             #region Extensions
//
//             public static Dictionary<string, Type> extendedInstanceTypes = new();
//
//             public static Dictionary<Type, Type> bindInstanceTypes = new();
//
//             public static Type QueryExtendedGameItemType(string id)
//             {
//                 if (id.IsNullOrEmptyAfterTrim())
//                 {
//                     return null;
//                 }
//                 
//                 return extendedInstanceTypes.TryGetValue(id, out var type) ? type : null;
//             }
//
//             public static Type QueryBindInstanceType(Type prefabType)
//             {
//                 return bindInstanceTypes.TryGetValue(prefabType, out var type)
//                     ? type
//                     : null;
//             }
//
//             public static void RefreshExtendedGameItems()
//             {
//                 var generalSetting = GameCoreSettingBase
//                     .FindGeneralSetting<GameItemGeneralSetting>();
//
//                 if (generalSetting == null)
//                 {
//                     Debug.LogWarning($"找不到{typeof(GameItem)}对应的通用设置");
//                     return;
//                 }
//
//                 extendedInstanceTypes.Clear();
//
//                 foreach (var currentGameItemType in typeof(GameItem).GetDerivedClasses(false,
//                              false))
//                 {
//                     if (currentGameItemType.IsAbstract)
//                     {
//                         continue;
//                     }
//
//                     string id = GetRegisteredID(currentGameItemType);
//
//                     if (id.IsNullOrEmptyAfterTrim())
//                     {
//                         continue;
//                     }
//
//                     if (generalSetting.ContainsPrefabID(id) == false)
//                     {
//                         Debug.LogWarning($"通用设置里不包含{currentGameItemType}的" +
//                                           $"{REGISTERED_ID_NAME}:{id}对应的预制体");
//                     }
//
//                     if (extendedInstanceTypes.TryGetValue(id, out var extendedType))
//                     {
//                         if (currentGameItemType.IsDerivedFrom(extendedType, false) ==
//                             false)
//                         {
//                             Debug.LogWarning($"{currentGameItemType.FullName}重复注册" +
//                                               $"扩展实例id:{id}，" +
//                                               $"原先注册的扩展实例" +
//                                               $"{extendedType.FullName}被覆盖");
//                         }
//                     }
//
//                     extendedInstanceTypes[id] = currentGameItemType;
//                 }
//
//                 bindInstanceTypes.Clear();
//
//                 foreach (var prefabType in GetDerivedPrefabsWithoutRegisteredID())
//                 {
//                     if (prefabType.IsAbstract)
//                     {
//                         continue;
//                     }
//
//                     var bindInstanceType = GetBindInstanceType(prefabType);
//
//                     if (bindInstanceType == null)
//                     {
//                         continue;
//                     }
//
//                     bindInstanceTypes[prefabType] = bindInstanceType;
//                 }
//
//                 var bindInstanceTypeDictByPrefabType =
//                     bindInstanceTypes.BuildValuesDictionary();
//
//                 foreach (var (bindInstanceType, prefabTypes) in
//                          bindInstanceTypeDictByPrefabType)
//                 {
//                     if (prefabTypes.Count > 1)
//                     {
//                         var prefabTypesName = prefabTypes
//                             .Select(prefabType => prefabType.FullName).Join(", ");
//
//                         Debug.LogWarning(
//                             $"有多个预制体类型:{prefabTypesName}" +
//                             $"绑定了实体类型:{bindInstanceType}");
//                     }
//                 }
//             }
//
//             #endregion
//
//             #region Create
//
//             /// <summary>
//             /// 当被创建时调用
//             /// </summary>
//             protected virtual void OnCreate()
//             {
//                 if (GameItemPrefab.gameTypeAbandoned == false)
//                 {
//                     uniqueGameType = origin.uniqueGameType;
//                 }
//             }
//
//             /// <summary>
//             /// 创建新实例，会自动转换成注册的Class
//             /// </summary>
//             /// <param name="id">需要创建的实例ID</param>
//             /// <returns></returns>
//             public static TInstance Create(string id)
//             {
//                 TInstance newGameItem;
//
//                 var prefab = GameItemPrefab.GetPrefabStrictly(id);
//
//                 if (extendedInstanceTypes.TryGetValue(id, out var extendedType))
//                 {
//                     newGameItem = (TInstance)Activator.CreateInstance(extendedType);
//                 }
//                 else
//                 {
//                     if (bindInstanceTypes.TryGetValue(prefab.GetType(),
//                             out var bindType))
//                     {
//                         newGameItem = (TInstance)Activator.CreateInstance(bindType);
//                     }
//                     else
//                     {
//                         newGameItem =
//                             (TInstance)Activator.CreateInstance(typeof(TInstance));
//                     }
//                 }
//
//                 newGameItem.id = id;
//                 newGameItem.origin = prefab;
//                 newGameItem.OnCreate();
//
//                 return newGameItem;
//             }
//
//             public static T Create<T>(string id) where T : TInstance
//             {
//                 var result = Create(id) as T;
//
//                 Note.note.AssertIsNotNull(result, nameof(result));
//
//                 return result;
//             }
//
//             public static TInstance CreateRandom()
//             {
//                 var prefab = GameItemPrefab.GetRandomPrefab();
//
//                 return Create(prefab.id);
//             }
//
//             public static T CreateRandom<T>() where T : TInstance
//             {
//                 var prefabs = GameItemPrefab.GetAllPrefabs();
//
//                 var ids = new List<string>();
//
//                 foreach (var prefab in prefabs)
//                 {
//                     if (prefab.actualInstanceType.IsAssignableFrom(typeof(T)))
//                     {
//                         ids.Add(prefab.id);
//                     }
//                 }
//
//                 return Create<T>(ids.Choose());
//             }
//
//             #endregion
//
//             #region Static Init
//
//             public static void Init()
//             {
//                 if (isInitialized)
//                 {
//                     return;
//                 }
//
//                 Debug.Log($"正在加载{typeof(TPrefab)}的实例扩展和绑定");
//
//                 RefreshExtendedGameItems();
//
//                 Debug.Log($"一共加载了{extendedInstanceTypes.Count}个自定义实例扩展类");
//
//                 Debug.Log($"一共加载了{bindInstanceTypes.Count}个默认实例绑定类");
//
//                 Debug.Log($"{typeof(TPrefab)}的实例扩展和绑定加载结束");
//
//                 isInitialized = true;
//             }
//
//             #endregion
//         }
//
//         [JsonObject(MemberSerialization.OptIn)]
//         public new abstract class GameItemPrefab :
//             GamePrefabCoreBundle<TPrefab, TGeneralSetting>.GameItemPrefab
//         {
//             #region Extension
//
//             [LabelText("扩展实例类"), TabGroup(TAB_GROUP_NAME, EXTENDED_CLASS_CATEGORY)]
//             [ShowInInspector]
//             [EnableGUI]
//             [HideIfNull]
//             [PropertyOrder(-5000)]
//             public Type extendedGameItemType => GameItem.QueryExtendedGameItemType(id);
//
//             [LabelText("绑定实例类"), TabGroup(TAB_GROUP_NAME, EXTENDED_CLASS_CATEGORY)]
//             [ShowInInspector]
//             [EnableGUI]
//             [HideIfNull]
//             [PropertyOrder(-4000)]
//             public Type bindGameItemType => GameItem.QueryBindInstanceType(GetType());
//
//             public Type actualInstanceType
//             {
//                 get
//                 {
//                     if (extendedGameItemType != null)
//                     {
//                         return extendedGameItemType;
//                     }
//
//                     if (bindGameItemType != null)
//                     {
//                         return bindGameItemType;
//                     }
//
//                     return typeof(TInstance);
//                 }
//             }
//
//             #endregion
//
//             #region GUI
//
// #if UNITY_EDITOR
//             [Button("打开实例脚本"), HorizontalGroup(OPEN_SCRIPT_CATEGORY)]
//             private void OpenInstanceScript()
//             {
//                 actualInstanceType.OpenScriptOfType();
//             }
// #endif
//
//             #region Extension
//
//             [Button("打印绑定的实例类型"), TabGroup(TAB_GROUP_NAME, EXTENDED_CLASS_CATEGORY)]
//             private void PrintBindInstanceType()
//             {
//                 Debug.LogWarning(GetBindInstanceType(GetType()));
//             }
//
//             #endregion
//
//             #endregion
//
//             #region Create
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public TInstance CreateInstance()
//             {
//                 return GameItem.Create(id);
//             }
//
//             #endregion
//         }
//
//         public new abstract class GameItemGeneralSetting :
//             GamePrefabCoreBundle<TPrefab, TGeneralSetting>.GameItemGeneralSetting
//         {
//             [LabelText("扩展实例类"), TabGroup(TAB_GROUP_NAME, EXTENDED_TYPES_SETTING_CATEGORY)]
//             [ReadOnly]
//             [EnableGUI]
//             [ShowInInspector]
//             public Dictionary<string, Type> ExtendedInstanceTypes => GameItem.extendedInstanceTypes;
//
//             [LabelText("绑定实例类"), TabGroup(TAB_GROUP_NAME, EXTENDED_TYPES_SETTING_CATEGORY)]
//             [ReadOnly]
//             [EnableGUI]
//             [ShowInInspector]
//             public Dictionary<Type, Type> bindInstanceTypes =>
//                 GameItem.bindInstanceTypes;
//
//             //[LabelText("实例类的默认泛型类别"), TitleGroup(EXTENDED_TYPES_SETTING_CATEGORY)]
//             //public List<Type> defaultGenericTypesOfInstanceType = new();
//
//             #region GUI
//
//             protected override void OnInspectorInit()
//             {
//                 base.OnInspectorInit();
//
//                 GameItem.RefreshExtendedGameItems();
//             }
//
//             #endregion
//
//             protected override void OnPreInit()
//             {
//                 base.OnPreInit();
//
//                 GameItem.Init();
//             }
//         }
//     }
//
//     public class
//         SimpleGameItemBundle<TPrefab, TGeneralSetting> :
//             GamePrefabCoreBundle<TPrefab, TGeneralSetting>
//         where TPrefab : SimpleGameItemBundle<TPrefab, TGeneralSetting, SimpleGameItemBundle<TPrefab, TGeneralSetting>.GameItem>.
//         GameItemPrefab
//         where TGeneralSetting : SimpleGameItemBundle<TPrefab, TGeneralSetting,
//             SimpleGameItemBundle<TPrefab, TGeneralSetting>.GameItem>.GameItemGeneralSetting
//     {
//         public abstract class GameItem : SimpleGameItemBundle<TPrefab,
//             TGeneralSetting, GameItem>.GameItem
//         {
//
//         }
//
//         [JsonObject(MemberSerialization.OptIn)]
//         public new abstract class GameItemPrefab : SimpleGameItemBundle<TPrefab,
//             TGeneralSetting, GameItem>.GameItemPrefab
//         {
//
//         }
//
//         public new abstract class GameItemGeneralSetting : SimpleGameItemBundle<
//             TPrefab,
//             TGeneralSetting, GameItem>.GameItemGeneralSetting
//         {
//
//         }
//     }
// }
