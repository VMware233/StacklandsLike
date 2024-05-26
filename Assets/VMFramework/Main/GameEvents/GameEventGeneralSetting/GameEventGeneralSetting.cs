using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public sealed partial class GameEventGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(GameEventConfig);

        public override string gameItemName => typeof(GameEvent<>).Name;

        #endregion

        [field: LabelText("KeyCode翻译"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [field: SerializeField]
        public DictionaryConfigs<KeyCode, KeyCodeTranslation> keyCodeTranslations
        {
            get;
            private set;
        } = new();
        
        #region Init & CheckSettings

        public override void CheckSettings()
        {
            base.CheckSettings();

            keyCodeTranslations ??= new();
            keyCodeTranslations.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            keyCodeTranslations.Init();
        }

        #endregion
        
        public string GetKeyCodeName(KeyCode keyCode, KeyCodeToStringMode mode)
        {
            if (keyCodeTranslations.TryGetConfig(keyCode, out var translation))
            {
                return translation.translation;
            }

            return keyCode.ConvertToString(mode);
        }
    }
}