#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    internal class GamePrefabViewerContainer : SimpleOdinEditorWindowContainer
    {
        [Searchable]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        [ShowInInspector]
        public List<IGamePrefab> gamePrefabs = new();
        
        [Searchable]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        [ShowInInspector]
        public List<string> gamePrefabIDs = new();

        public override void Init()
        {
            base.Init();
            
            gamePrefabs.AddRange(GamePrefabManager.GetAllGamePrefabs());
            gamePrefabIDs.AddRange(GamePrefabManager.GetAllIDs());
            
            GamePrefabManager.OnGamePrefabRegisteredEvent += OnRegisterGamePrefab;
            GamePrefabManager.OnGamePrefabUnregisteredEvent += OnUnregisterGamePrefab;
        }

        public void OnRegisterGamePrefab(IGamePrefab gamePrefab)
        {
            gamePrefabs.Add(gamePrefab);
            gamePrefabIDs.Add(gamePrefab.id);
        }

        public void OnUnregisterGamePrefab(IGamePrefab gamePrefab)
        {
            gamePrefabs.Remove(gamePrefab);
            gamePrefabIDs.Remove(gamePrefab.id);
        }
    }
}
#endif