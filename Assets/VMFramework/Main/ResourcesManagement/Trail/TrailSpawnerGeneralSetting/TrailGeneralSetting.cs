using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public sealed partial class TrailGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(TrailPreset);

        #endregion

        [HideLabel, TabGroup(TAB_GROUP_NAME, MISCELLANEOUS_CATEGORY)]
        public ContainerChooser container = new();
    }
}
