using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class GameTypedGamePrefab : GamePrefab, IGameTypedGamePrefab
    {
        #region Configs

#if UNITY_EDITOR
        [LabelText("Game Types"),
         TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY, SdfIconType.Info, TextColor = "blue")]
        [OnCollectionChanged(nameof(OnInitialGameTypesIDChangedGUI))]
        [PropertyOrder(-6000)]
        [GameTypeID]
        [ListDrawerSettings(ShowFoldout = false)]
#endif
        [SerializeField, JsonProperty(Order = -6000)]
        private List<string> _initialGameTypesID = new();

        #endregion

        #region Config Properties

        public IList<string> initialGameTypesID
        {
            get => _initialGameTypesID;
            init => _initialGameTypesID = value.ToList();
        }

        #endregion

        #region Game Type

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY, SdfIconType.Bug, TextColor = "orange")]
        [ShowInInspector]
        private GameTypeSet _gameTypeSet;

        public IGameTypeSet gameTypeSet
        {
            get
            {
                if (_gameTypeSet != null)
                {
                    return _gameTypeSet;
                }
                
                _gameTypeSet = new(this);

                _gameTypeSet.OnAddLeafGameType += OnAddLeafGameType;
                _gameTypeSet.OnRemoveLeafGameType += OnRemoveLeafGameType;

                _gameTypeSet.AddGameTypes(initialGameTypesID);
                
                return _gameTypeSet;
            }
        }

        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ShowInInspector]
        [HideInEditorMode]
        [JsonIgnore]
        public GameType uniqueGameType { get; private set; }

        private void OnAddLeafGameType(IReadOnlyGameTypeSet gameTypeSet, GameType gameType)
        {
            uniqueGameType ??= gameType;
        }

        private void OnRemoveLeafGameType(IReadOnlyGameTypeSet gameTypeSet, GameType gameType)
        {
            if (uniqueGameType == gameType)
            {
                uniqueGameType = null;

                if (_gameTypeSet.HasGameType())
                {
                    uniqueGameType = _gameTypeSet.leafGameTypes.Choose();
                }
            }
        }

        #endregion
        
        #region Init & Check

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (initialGameTypesID == null)
            {
                return;
            }

            foreach (var gameTypeID in initialGameTypesID)
            {
                if (GameType.HasGameType(gameTypeID) == false)
                {
                    Debug.LogWarning($"{this} : Game Type ID {gameTypeID} of does not exist.");
                }
            }
        }

        #endregion
    }
}