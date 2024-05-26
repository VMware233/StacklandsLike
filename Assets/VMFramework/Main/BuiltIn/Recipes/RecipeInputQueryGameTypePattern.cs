using VMFramework.GameLogicArchitecture;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;

namespace VMFramework.Recipe
{
    public readonly struct RecipeInputQueryGameTypePattern : IRecipeInputQueryPattern
    {
        public readonly string gameTypeID;

        public RecipeInputQueryGameTypePattern(string gameTypeID)
        {
            this.gameTypeID = gameTypeID;
        }

        #region Cache

        private static Dictionary<string, HashSet<Recipe>> cache = new();

        void IRecipeInputQueryPattern.RegisterCache(Recipe recipe)
        {
            var gameType = GameType.GetGameType(gameTypeID);

            if (gameType == null)
            {
                Debug.LogWarning($"找不到ID为{gameTypeID}的游戏类型");
            }

            foreach (var child in gameType.PreorderTraverse(true))
            {
                if (child.id == GameType.ALL_ID)
                {
                    continue;
                }

                cache.TryAdd(child.id, new());

                cache[child.id].Add(recipe);
            }
        }

        [RecipeInputQueryHandler]
        private static IEnumerable<Recipe> GetRecipes(object item)
        {
            if (item is IContainerItem containerItem)
            {
                foreach (var leafGameTypeID in containerItem.gameTypeSet.leafGameTypesID)
                {
                    if (cache.TryGetValue(leafGameTypeID, out var recipes))
                    {
                        foreach (var recipe in recipes)
                        {
                            yield return recipe;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
