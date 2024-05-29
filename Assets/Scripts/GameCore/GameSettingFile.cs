using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using StackLandsLike.Tasks;
using StackLandsLike.UI;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public sealed class GameSettingFile : GameCoreSettingFile
    {
        public CardGeneralSetting cardGeneralSetting;
        public CardViewGeneralSetting cardViewGeneralSetting;
        public CardRecipeGeneralSetting cardRecipeGeneralSetting;
        public GameTimeGeneralSetting gameTimeGeneralSetting;
        public TaskGeneralSetting taskGeneralSetting;
    }
}