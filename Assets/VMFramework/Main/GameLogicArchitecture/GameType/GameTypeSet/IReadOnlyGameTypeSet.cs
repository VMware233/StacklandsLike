using System;
using System.Collections.Generic;
using System.Linq;

namespace VMFramework.GameLogicArchitecture
{
    public interface IReadOnlyGameTypeSet
    {
        public event Action<IReadOnlyGameTypeSet, GameType> OnAddGameType;
        public event Action<IReadOnlyGameTypeSet, GameType> OnAddLeafGameType;

        public event Action<IReadOnlyGameTypeSet, GameType> OnRemoveGameType;
        public event Action<IReadOnlyGameTypeSet, GameType> OnRemoveLeafGameType;
        
        public IEnumerable<GameType> gameTypes { get; }

        public IEnumerable<GameType> leafGameTypes { get; }

        public IEnumerable<string> gameTypesID { get; }

        public IEnumerable<string> leafGameTypesID { get; }

        public bool HasAnyGameType(IEnumerable<string> typeIDs)
        {
            return typeIDs.Any(HasGameType);
        }

        public bool HasAllGameTypes(IEnumerable<string> typeIDs)
        {
            return typeIDs.All(HasGameType);
        }

        public bool HasGameType(string typeID);

        public bool TryGetGameType(string typeID, out GameType gameType);

        public GameType GetGameType(string typeID);

        public bool HasGameType();
    }
}