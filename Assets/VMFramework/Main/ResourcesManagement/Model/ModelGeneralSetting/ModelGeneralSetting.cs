using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public sealed partial class ModelGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data
        
        public override Type baseGamePrefabType => typeof(ModelPreset);

        #endregion
    }
}
