using StackLandsLike.Cards;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public sealed class GameSetting : GameCoreSetting
    {
        public static GameSettingFile gameSettingFile => (GameSettingFile)gameCoreSettingsFile;
        
        public static CardGeneralSetting cardGeneralSetting => gameSettingFile.cardGeneralSetting;
    }
}