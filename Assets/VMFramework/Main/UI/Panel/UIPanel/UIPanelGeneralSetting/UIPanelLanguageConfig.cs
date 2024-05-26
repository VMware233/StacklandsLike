using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UIPanelLanguageConfig : BaseConfig, IIDOwner, INameOwner
    {
        [LabelText("语言代码")]
        [LocaleName]
        [IsNotNullOrEmpty]
        [SerializeField]
        private string localeCode;

        [LabelText("样式表")]
        [Required]
        public StyleSheet styleSheet;

        #region Init & Check

        public override void CheckSettings()
        {
            base.CheckSettings();

            styleSheet.AssertIsNotNull(nameof(styleSheet));
        }

        #endregion

        #region ID & Name Owner

        string IIDOwner<string>.id => localeCode;

        string INameOwner.name => localeCode;

        #endregion
    }
}