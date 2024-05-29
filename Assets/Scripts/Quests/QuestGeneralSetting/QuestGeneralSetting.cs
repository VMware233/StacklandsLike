using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Quests
{
    public sealed partial class QuestGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(QuestConfig);

        public override string gameItemName => nameof(Quest);

        #endregion
    }
}