using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using StackLandsLike.Quests;
using StackLandsLike.UI;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public sealed class GameSetting : GameCoreSetting
    {
        public static GameSettingFile gameSettingFile => (GameSettingFile)gameCoreSettingsFile;
        
        public static CardGeneralSetting cardGeneralSetting => gameSettingFile.cardGeneralSetting;
        
        public static CardViewGeneralSetting cardViewGeneralSetting => gameSettingFile.cardViewGeneralSetting;
        
        public static CardRecipeGeneralSetting cardRecipeGeneralSetting => gameSettingFile.cardRecipeGeneralSetting;
        
        public static GameTimeGeneralSetting gameTimeGeneralSetting => gameSettingFile.gameTimeGeneralSetting;
        
        public static QuestGeneralSetting questGeneralSetting => gameSettingFile.questGeneralSetting;
        
        public static GameStateGeneralSetting gameStateGeneralSetting => gameSettingFile.gameStateGeneralSetting;
        
        public static ScoreboardGeneralSetting scoreboardGeneralSetting => gameSettingFile.scoreboardGeneralSetting;
        
        public static QuestAndRecipeUIGeneralSetting questAndRecipeUIGeneralSetting => gameSettingFile.questAndRecipeUIGeneralSetting;
        
        public static SettlementScreenGeneralSetting settlementScreenGeneralSetting => gameSettingFile.settlementScreenGeneralSetting;
    }
}