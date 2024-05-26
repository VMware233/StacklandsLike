using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{ 
    public sealed partial class SpriteGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data
        
        public override Type baseGamePrefabType => typeof(SpritePreset);

        #endregion
    }
}
