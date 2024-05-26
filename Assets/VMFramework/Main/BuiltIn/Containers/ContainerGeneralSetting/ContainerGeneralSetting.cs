using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public sealed partial class ContainerGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(ContainerPreset);
        
        public override string gameItemName => nameof(Container);

        #endregion
    }
}
