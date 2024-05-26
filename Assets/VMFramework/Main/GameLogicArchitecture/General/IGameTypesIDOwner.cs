using System.Collections.Generic;

namespace VMFramework.GameLogicArchitecture
{
    public interface IGameTypesIDOwner
    {
        public IEnumerable<string> gameTypesID { get; }
    }
}