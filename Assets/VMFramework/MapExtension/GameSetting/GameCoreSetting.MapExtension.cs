using VMFramework.Map;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSetting
    {
        public static MapCoreGeneralSetting mapCoreGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.mapCoreGeneralSetting;
    }
}