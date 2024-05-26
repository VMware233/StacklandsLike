using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class GamePrefabWrapper : SerializedScriptableObject, INameOwner
    {
        public abstract IEnumerable<IGamePrefab> GetGamePrefabs();

        string INameOwner.name => this == null ? "Null" : name;
    }
}
