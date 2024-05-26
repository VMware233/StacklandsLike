using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    public abstract partial class GamePrefab : IGamePrefab
    {
        #region Constants

        protected const string ACTIVE_STATE_AND_DEBUGGING_MODE_HORIZONTAL_GROUP =
            "ActiveStateAndDebuggingModeHorizontalGroup";

        protected const string TAB_GROUP_NAME = "TabGroup";

        protected const string BASIC_SETTING_CATEGORY = "Basic";

        protected const string TOOLS_CATEGORY = "Tools";

        protected const string RUNTIME_DATA_CATEGORY = "Runtime Data";

        #endregion

        #region Configs

        [HideInInspector]
        [SerializeField, JsonProperty(Order = -10000)]
        private string _id;

        [LabelText(SdfIconType.Activity)]
        [JsonProperty(Order = -9000), PropertyOrder(-9000)]
        [HorizontalGroup(ACTIVE_STATE_AND_DEBUGGING_MODE_HORIZONTAL_GROUP)]
        public bool isActive = true;

        [LabelText(SdfIconType.BugFill)]
        [JsonProperty(Order = -8000), PropertyOrder(-8000)]
        [HorizontalGroup(ACTIVE_STATE_AND_DEBUGGING_MODE_HORIZONTAL_GROUP)]
        public bool isDebugging = false;

        #endregion

        #region Properties

        [LabelText("ID", SdfIconType.Globe)]
        [ShowInInspector]
        [IsNotNullOrEmpty(DrawCurrentRect = true)]
        [IsGamePrefabID]
        [ValidateIsNot(content: IGamePrefab.NULL_ID, DrawCurrentRect = true)]
        [Placeholder("@GetIDPlaceholderText()")]
        [PropertyOrder(-10000)]
        public string id
        {
            get => _id;
            set
            {
                var oldID = _id;
                _id = value;
                OnIDChangedEvent?.Invoke(this, oldID, _id);
            }
        }

        public virtual Type gameItemType => null;

        public bool initDone { get; private set; } = false;

        #endregion

        #region Events

        public event Action<IGamePrefab, string, string> OnIDChangedEvent; 

        #endregion

        #region ID Prefix and Suffix

        protected virtual string idPrefix => null;
        
        protected virtual string idSuffix => null;

        public bool isIDStartsWithPrefix
        {
            get
            {
                if (id.IsNullOrEmptyAfterTrim() || idPrefix.IsNullOrEmptyAfterTrim())
                {
                    return true;
                }

                var prefix = idPrefix.TrimEnd(' ', '_').TrimStart(' ', '_');
                
                return id.StartsWith(prefix);
            }
        }

        public bool isIDEndsWithSuffix
        {
            get
            {
                if (id.IsNullOrEmptyAfterTrim() || idSuffix.IsNullOrEmptyAfterTrim())
                {
                    return true;
                }

                var suffix = idSuffix.TrimEnd(' ', '_').TrimStart(' ', '_');
                
                return id.EndsWith(suffix);
            }
        }

        #endregion

        #region Interface Implementations

        string IIDOwner<string>.id => id;

        string INameOwner.name => id;

        bool IGamePrefab.isActive
        {
            get => isActive;
            set => isActive = value;
        }

        bool IGamePrefab.isDebugging
        {
            get => isDebugging;
            set => isDebugging = value;
        }

        #endregion
        
        #region To String

        public override string ToString()
        {
            return $"({GetType()}, id:{id})";
        }

        #endregion
    }
}
