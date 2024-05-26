using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core.Linq;

namespace VMFramework.GameLogicArchitecture
{
    public interface IGameTypeSet : IReadOnlyGameTypeSet
    {
        public IGameTypeOwner owner { get; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddGameTypes(IEnumerable<string> gameTypeIDs)
        {
            gameTypeIDs.Examine(AddGameType);
        }

        public void AddGameType(string gameTypeID);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveGameTypes(IEnumerable<string> gameTypeIDs)
        {
            gameTypeIDs.Examine(RemoveGameType);
        }

        public void RemoveGameType(string gameTypeID);
    }
}