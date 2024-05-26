#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GeneralSetting
    {
        [LabelText("类型")]
        [PropertyOrder(-100000), ShowInInspector]
        [OnInspectorInit(nameof(OnInspectorInit))]
        private Type generalSettingType => GetType(); 
        
        protected virtual void OnInspectorInit()
        {
            AutoConfigureLocalizedString(new()
            {
                defaultTableName = defaultLocalizationTableName,
                save = true
            });
        }

        public virtual void InitializeOnLoad()
        {
            
        }
    }
}
#endif