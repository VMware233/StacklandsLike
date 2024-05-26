using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Recipe
{
    public interface IRecipe : IGameTypedGamePrefab
    {
        public IEnumerable<IRecipeInputQueryPattern> GetInputQueryPatterns();

        public IEnumerable<IRecipeOutputQueryPattern> GetOutputQueryPatterns();
    }
}