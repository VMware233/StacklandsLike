using System;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [GameInitializerRegister(typeof(GameInitializationProcedure))]
    [Preserve]
    public sealed class UIPanelManagerInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            Debug.Log("正在创建初始的UI面板");

            foreach (var uiPanelPreset in GamePrefabManager.GetAllGamePrefabs<IUIPanelPreset>())
            {
                if (uiPanelPreset.isUnique)
                {
                    UIPanelManager.CreatePanel(uiPanelPreset.id);
                }
                else if (uiPanelPreset.prewarmCount > 0)
                {
                    for (int i = 0; i < uiPanelPreset.prewarmCount; i++)
                    {
                        UIPanelManager.CreatePanel(uiPanelPreset.id);
                    }
                }
            }
            
            onDone();
        }
    }
}