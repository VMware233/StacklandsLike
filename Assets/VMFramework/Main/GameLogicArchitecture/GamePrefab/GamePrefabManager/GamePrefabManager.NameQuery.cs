#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.GameLogicArchitecture
{
    public static partial class GamePrefabManager
    {
        public static ValueDropdownItem GetNameIDDropDownItem(this IGamePrefab prefab)
        {
            return new(prefab.name, prefab.id);
        }
        
        /// <summary>
        /// 获取所有<see cref="IGamePrefab"/>的名称和ID，一般用于Odin插件里的ValueDropdownAttribute
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ValueDropdownItem> GetGamePrefabNameList()
        {
            foreach (var prefab in allGamePrefabsByID.Values)
            {
                if (prefab is not null)
                {
                    yield return prefab.GetNameIDDropDownItem();
                }
            }
        }

        /// <summary>
        /// 获取特定Type对应的类或其派生类的<see cref="IGamePrefab"/>的名称和ID，
        /// 一般用于Odin插件里的ValueDropdown Attribute
        /// </summary>
        /// <param name="specificTypes"></param>
        /// <returns></returns>
        public static IEnumerable<ValueDropdownItem> GetGamePrefabNameListByType(
            params Type[] specificTypes)
        {
            foreach (var gamePrefab in GetGamePrefabsByTypes(specificTypes))
            {
                yield return gamePrefab.GetNameIDDropDownItem();
            }
        }

        /// <summary>
        /// 获取特定Type对应的类或其派生类的<see cref="IGamePrefab"/>的名称和ID，
        /// 一般用于Odin插件里的ValueDropdown Attribute
        /// </summary>
        /// <param name="specificType"></param>
        /// <returns></returns>
        public static IEnumerable<ValueDropdownItem> GetGamePrefabNameListByType(Type specificType)
        {
            foreach (var gamePrefab in GetGamePrefabsByType(specificType))
            {
                yield return gamePrefab.GetNameIDDropDownItem();
            }
        }

        /// <summary>
        /// 获取特定的GameItemType对应的<see cref="IGamePrefab"/>的名称和ID，
        /// 一般用于Odin插件里的ValueDropdown Attribute
        /// </summary>
        /// <param name="gameItemType"></param>
        /// <returns></returns>
        public static IEnumerable<ValueDropdownItem> GetGamePrefabNameListByGameItemType(Type gameItemType)
        {
            foreach (var gamePrefab in GetGamePrefabsByGameItemType(gameItemType))
            {
                yield return gamePrefab.GetNameIDDropDownItem();
            }
        }
    }
}

#endif