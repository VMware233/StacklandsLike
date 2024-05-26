using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static class GameTypeNameUtility
    {
        public static string GetGameTypeName(string gameTypeId)
        {
            return GameType.GetGameType(gameTypeId)?.name;
        }
        
        /// <summary>
        /// 获取所有叶节点<see cref="GameType"/>的名称和ID，一般用于Odin插件里的<see cref="ValueDropdownAttribute"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ValueDropdownItem> GetGameTypeNameList()
        {
            foreach (var gameType in GameType.GetAllLeafGameTypes())
            {
                var name = gameType.name;

                foreach (var parent in gameType.TraverseToRoot(false))
                {
                    if (parent.isRoot)
                    {
                        continue;
                    }
                    
                    name = parent.name + " / " + name;
                }
                
                yield return new ValueDropdownItem(name, gameType.id);
            }
        }

        /// <summary>
        /// 获取所有叶节点<see cref="GameType"/>的ID列表，一般用于Odin插件里的<see cref="ValueDropdownAttribute"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetGameTypeIDList()
        {
            return GameType.GetAllLeafGameTypes().Select(gameType => gameType.id);
        }

        /// <summary>
        /// 获取所有<see cref="GameType"/>的名称和ID，一般用于Odin插件里的<see cref="ValueDropdownAttribute"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ValueDropdownItem> GetAllGameTypeNameList()
        {
            foreach (var gameType in GameType.GetAllGameTypes())
            {
                var name = gameType.name;

                foreach (var parent in gameType.TraverseToRoot(false))
                {
                    if (parent.isRoot)
                    {
                        continue;
                    }
                    
                    name = parent.name + " / " + name;
                }
                
                yield return new ValueDropdownItem(name, gameType.id);
            }
        }

        /// <summary>
        /// 获取所有<see cref="GameType"/>的ID列表，一般用于Odin插件里的<see cref="ValueDropdownAttribute"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAllGameTypeIDList()
        {
            return GameType.GetAllGameTypes().Select(gameType => gameType.id);
        }
    }
}