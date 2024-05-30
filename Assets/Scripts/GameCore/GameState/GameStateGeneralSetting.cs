using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace StackLandsLike.GameCore
{
    public sealed partial class GameStateGeneralSetting : GeneralSetting
    {
        [GamePrefabID(typeof(BoolInputGameEventConfig))]
        public string toggleGameStateGameEventID;
        
        [GamePrefabID(typeof(BoolInputGameEventConfig))]
        public string endGameStateGameEventID;
    }
}