#if UNITY_EDITOR
using VMFramework.Configuration;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefab : IConfig
    {
        #region On Inspector Init

        protected virtual void OnInspectorInit()
        {
            
        }

        void IGamePrefab.OnInspectorInit()
        {
            OnInspectorInit();
        }
        
        void IConfig.OnInspectorInit()
        {
            OnInspectorInit();
        }

        #endregion
        
        #region ID

        private string GetIDPlaceholderText()
        {
            const string placeholderText = "请输入ID";
            if (idSuffix.IsNullOrEmptyAfterTrim())
            {
                return placeholderText;
            }

            return placeholderText + $"并以_{idSuffix}结尾";
        }

        #endregion
    }
}
#endif