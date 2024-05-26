using System.Collections.Generic;

namespace VMFramework.GameLogicArchitecture
{
    public interface IGameTypedGamePrefab : IGamePrefab, IGameTypeOwner
    {
        public GameType uniqueGameType { get; }
        
        public IList<string> initialGameTypesID { get; }
    }
}