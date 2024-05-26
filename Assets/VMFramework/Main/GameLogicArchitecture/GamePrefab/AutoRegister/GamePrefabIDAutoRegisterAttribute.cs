using System;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    /// <summary>
    /// 用于挂在static类里的const字符串上，
    /// 以便在编辑器或者运行时自动创建ID为此字符串值的<see cref="IGamePrefab"/>，
    /// 需要提供GamePrefab的类型，并且提供的类型必须实现<see cref="IGamePrefab"/>接口。
    /// 如果需要在自动注册时提供Callback，可以实现<see cref="IGamePrefabAutoRegisterProvider"/>接口。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class GamePrefabIDAutoRegisterAttribute : Attribute
    {
        public readonly Type GamePrefabType;
        
        public GamePrefabIDAutoRegisterAttribute(Type gamePrefabType)
        {
            if (gamePrefabType.IsClass == false)
            {
                throw new ArgumentException("The provided type must be a class.");
            }

            if (gamePrefabType.IsAbstract)
            {
                throw new ArgumentException("The provided type must not be abstract.");
            }

            if (gamePrefabType.IsDerivedFrom<IGamePrefab>(false) == false)
            {
                throw new ArgumentException("The provided type must implement the IGamePrefab interface.");
            }
            
            GamePrefabType = gamePrefabType;
        }
    }
}