using System.Collections.Generic;
using System.Linq;
using VMFramework.Containers;

namespace VMFramework.Recipe
{
    public readonly struct RecipeInputQueryIDPattern : IRecipeInputQueryPattern
    {
        public readonly string id;

        public RecipeInputQueryIDPattern(string id)
        {
            this.id = id;
        }

        #region Cache

        private static Dictionary<string, HashSet<Recipe>> cache = new();

        void IRecipeInputQueryPattern.RegisterCache(Recipe recipe)
        {
            cache.TryAdd(id, new());
            cache[id].Add(recipe);
        }

        [RecipeInputQueryHandler]
        private static IEnumerable<Recipe> GetRecipes(object item)
        {
            if (item is IContainerItem containerItem)
            {
                if (cache.TryGetValue(containerItem.id, out var recipes))
                {
                    return recipes;
                }
            }

            return Enumerable.Empty<Recipe>();
        }

        #endregion
    }
}
