using System;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Property
{
    [GameInitializerRegister(typeof(GameInitializationProcedure))]
    public class TooltipPropertyInitializer : IGameInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            Debug.Log("Initializing Tooltip Property");
            
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs())
            {
                if (gamePrefab.gameItemType == null)
                {
                    continue;
                }

                if (gamePrefab.gameItemType.IsAbstract)
                {
                    continue;
                }

                foreach (var configRuntime in TooltipPropertyManager.GetTooltipPropertyConfigsRuntime(
                             gamePrefab.gameItemType))
                {
                    TooltipPropertyManager.AddTooltipPropertyConfigRuntime(gamePrefab.id, configRuntime);
                }
            }

            onDone();
        }
    }
}