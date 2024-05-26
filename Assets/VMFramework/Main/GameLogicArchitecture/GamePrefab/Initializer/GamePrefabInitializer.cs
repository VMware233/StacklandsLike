using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(typeof(CoreInitializationProcedure))]
    [Preserve]
    public class GamePrefabInitializer : IGameInitializer
    {
        async void IInitializer.OnPreInit(Action onDone)
        {
            GamePrefabManager.Clear();
            
            foreach (var generalSetting in GameCoreSetting.GetAllGeneralSettings())
            {
                if (generalSetting is IInitialGamePrefabProvider initialGamePrefabProvider)
                {
                    foreach (var gamePrefab in initialGamePrefabProvider.GetInitialGamePrefabs())
                    {
                        GamePrefabManager.RegisterGamePrefab(gamePrefab);
                    }
                }
            }
            
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs())
            {
                gamePrefab.CheckSettings();
            }

            int totalCount = GamePrefabManager.GetGamePrefabsCount();
            int currentCount = 0;
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs().ToList())
            {
                gamePrefab.OnPreInit(() => currentCount++);

                await UniTask.NextFrame();
            }
            
            await UniTask.WaitUntil(() => currentCount >= totalCount);

            onDone();
        }

        async void IInitializer.OnInit(Action onDone)
        {
            int totalCount = GamePrefabManager.GetGamePrefabsCount();
            int currentCount = 0;
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs().ToList())
            {
                gamePrefab.OnInit(() => currentCount++);
                
                await UniTask.NextFrame();
            }
            
            await UniTask.WaitUntil(() => currentCount >= totalCount);
            
            onDone();
        }

        async void IInitializer.OnPostInit(Action onDone)
        {
            int totalCount = GamePrefabManager.GetGamePrefabsCount();
            int currentCount = 0;
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs().ToList())
            {
                gamePrefab.OnPostInit(() => currentCount++);
                
                await UniTask.NextFrame();
            }
            
            await UniTask.WaitUntil(() => currentCount >= totalCount);
            
            onDone();
        }

        async void IInitializer.OnInitComplete(Action onDone)
        {
            int totalCount = GamePrefabManager.GetGamePrefabsCount();
            int currentCount = 0;
            foreach (var gamePrefab in GamePrefabManager.GetAllGamePrefabs().ToList())
            {
                gamePrefab.OnInitComplete(() => currentCount++);
                
                await UniTask.NextFrame();
            }
            
            await UniTask.WaitUntil(() => currentCount >= totalCount);
            
            onDone();
        }
    }
}