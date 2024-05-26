using System;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    /// <summary>
    /// 用于挂在某个特定的实现了<see cref="IGamePrefab"/>的类上，
    /// 以便在编辑器或者运行时自动创建并注册该<see cref="IGamePrefab"/>
    /// 如果需要在自动注册时提供Callback，可以实现<see cref="IGamePrefabAutoRegisterProvider"/>接口。
    /// <code>
    /// [GamePrefabTypeAutoRegister(DEFAULT_ID)]
    /// public class TestCardConfig : LocalizedGamePrefab, IGamePrefabAutoRegisterProvider
    /// {
    ///     public const string DEFAULT_ID = "test_card";
    ///
    ///     void IGamePrefabAutoRegisterProvider.OnGamePrefabAutoRegister()
    ///     {
    ///         Debug.Log($"Is Auto Registering {nameof(TestCardConfig)} : {id}");
    ///     }
    /// }
    /// </code>>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class GamePrefabTypeAutoRegisterAttribute : Attribute
    {
        public readonly string ID;
        
        public GamePrefabTypeAutoRegisterAttribute(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentException("ID cannot be null or empty.");
            }
            
            ID = id;
        }
    }
}