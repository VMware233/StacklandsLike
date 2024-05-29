using System;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Tasks
{
    public sealed partial class TaskGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(TaskConfig);

        public override string gameItemName => nameof(Task);

        #endregion
    }
}