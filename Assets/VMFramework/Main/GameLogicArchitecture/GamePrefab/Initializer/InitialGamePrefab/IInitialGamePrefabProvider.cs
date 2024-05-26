using System.Collections.Generic;

namespace VMFramework.GameLogicArchitecture
{
    public interface IInitialGamePrefabProvider
    {
        public IEnumerable<IGamePrefab> GetInitialGamePrefabs();
    }
}