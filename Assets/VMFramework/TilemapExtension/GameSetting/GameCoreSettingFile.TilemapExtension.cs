using Sirenix.OdinInspector;
using VMFramework.ExtendedTilemap;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingFile
    {
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ExtendedRuleTileGeneralSetting extendedRuleTileGeneralSetting;
    }
}