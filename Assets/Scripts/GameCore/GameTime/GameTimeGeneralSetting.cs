using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public sealed class GameTimeGeneralSetting : GeneralSetting
    {
        [MinValue(1)]
        public int totalTicksPerDay = 12000;
    }
}