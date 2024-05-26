using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Map
{
    public sealed partial class MapCoreGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(MapCoreConfiguration);

        #endregion
    }
}
