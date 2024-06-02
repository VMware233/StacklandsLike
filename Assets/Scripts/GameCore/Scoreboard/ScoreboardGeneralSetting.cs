using System.Collections.Generic;
using StackLandsLike.Cards;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace StackLandsLike.GameCore
{
    public sealed partial class ScoreboardGeneralSetting : GeneralSetting
    {
        [GamePrefabID(typeof(ICardConfig))]
        public HashSet<string> treesID;
    }
}