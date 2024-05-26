using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.ExtendedTilemap
{
    public sealed partial class ExtendedRuleTileGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(ExtendedRuleTile);

        #endregion

        [LabelText("默认的特定瓦片")]
        [GamePrefabID(typeof(ExtendedRuleTile))]
        [GUIColor(0.7f, 0.7f, 1)]
        public HashSet<string> defaultSpecificTiles = new();

        [LabelText("默认的非特定瓦片")]
        [GamePrefabID(typeof(ExtendedRuleTile))]
        [GUIColor(0.5f, 1f, 1)]
        public HashSet<string> defaultNotSpecificTiles = new();
    }
}
