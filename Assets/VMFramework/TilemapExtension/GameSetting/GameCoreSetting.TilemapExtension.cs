using VMFramework.ExtendedTilemap;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSetting
    {
        public static ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.extendedRuleTileGeneralSetting;
    }
}