#if UNITY_EDITOR
using System;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal class GameTypeEditorInitializer : IEditorInitializer
    {
        public void OnPreInit(Action onDone)
        {
            GameCoreSetting.gameTypeGeneralSetting.InitGameTypeInfo();
            
            onDone();
        }
    }
}
#endif