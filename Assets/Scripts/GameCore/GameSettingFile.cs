using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using StackLandsLike.Quests;
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
        public QuestGeneralSetting questGeneralSetting;
        public GameStateGeneralSetting gameStateGeneralSetting;
        public QuestAndRecipeUIGeneralSetting questAndRecipeUIGeneralSetting;
    }
}