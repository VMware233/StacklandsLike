using StackLandsLike.Cards;
using StackLandsLike.UI;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public sealed class GameSettingFile : GameCoreSettingFile
    {
        public CardGeneralSetting cardGeneralSetting;
        public CardViewGeneralSetting cardViewGeneralSetting;
        public CardRecipeGeneralSetting cardRecipeGeneralSetting;
    }
}