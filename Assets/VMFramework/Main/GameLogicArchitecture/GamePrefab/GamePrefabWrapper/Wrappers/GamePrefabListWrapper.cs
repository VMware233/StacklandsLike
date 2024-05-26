using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.GameLogicArchitecture
{
    public class GamePrefabListWrapper : GamePrefabWrapper
    {
        [LabelText("Game Prefabs")]
        public List<GamePrefab> gamePrefabs = new();

        public override IEnumerable<IGamePrefab> GetGamePrefabs()
        {
            return gamePrefabs;
        }
    }
}