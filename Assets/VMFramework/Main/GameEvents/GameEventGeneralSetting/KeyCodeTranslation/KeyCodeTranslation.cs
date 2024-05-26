using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.GameEvents
{
    public partial class KeyCodeTranslation : BaseConfig, IIDOwner<KeyCode>, INameOwner
    {
        [field: HideLabel]
        [field: SerializeField]
        public KeyCode keyCode { get;private set; }

        [field: LabelText("翻译")]
        [field: SerializeField]
        public LocalizedStringReference translation { get; private set; } = new();

        #region Constructor

        public KeyCodeTranslation()
        {
            
        }

        public KeyCodeTranslation(KeyCode keyCode, LocalizedStringReference translation)
        {
            this.keyCode = keyCode;
            this.translation = translation;
        }

        #endregion

        #region IIDOwner

        KeyCode IIDOwner<KeyCode>.id => keyCode;

        string INameOwner.name => keyCode.ToString();

        #endregion
    }
}