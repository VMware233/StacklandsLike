using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        [field: TabGroup(TAB_GROUP_NAME, GAME_TYPE_CATEGORY)]
        [field: InfoBox("GamePrefab's Game Type is disabled", VisibleIf = "@!gamePrefabGameTypeEnabled")]
        [field: GameTypeID]
        [field: EnableIf(nameof(gamePrefabGameTypeEnabled))]
        [field: SerializeField]
        [JsonProperty]
        public string defaultGameType { get; private set; }

        private bool gamePrefabGameTypeEnabled =>
            baseGamePrefabType.IsDerivedFrom<IGameTypedGamePrefab>(false);

        #region JSON

        public bool ShouldSerializedefaultGameType()
        {
            return gamePrefabGameTypeEnabled;
        }

        #endregion
    }
}