#if UNITY_EDITOR
using System;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal class GameSettingEditorInitializer : IEditorInitializer
    {
        public void OnBeforeInit(Action onDone)
        {
            GameCoreSettingFile.CheckGlobal();
            
            GameCoreSetting.gameCoreSettingsFile.AutoFindSetting();
            
            foreach (var generalSetting in GameCoreSetting.GetAllGeneralSettings())
            {
                if (generalSetting == null)
                {
                    continue;
                }
                
                generalSetting.InitializeOnLoad();
            }
            
            onDone();
        }
    }
}
#endif