using System;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;

namespace VMFramework.ResourcesManagement
{
    public sealed partial class AudioGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(AudioPreset);

        #endregion

        [HideLabel, TabGroup(TAB_GROUP_NAME, MISCELLANEOUS_CATEGORY)]
        public ContainerChooser container = new();
    }
}
