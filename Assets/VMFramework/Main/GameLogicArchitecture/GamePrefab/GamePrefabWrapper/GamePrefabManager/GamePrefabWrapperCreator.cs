#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GamePrefabWrapperCreator
    {
        public static event Action<GamePrefabWrapper> OnGamePrefabWrapperCreated;

        private static IGamePrefab CreateDefaultGamePrefab(string id, Type gamePrefabType)
        {
            if (gamePrefabType == null)
            {
                throw new ArgumentNullException(nameof(gamePrefabType));
            }

            if (gamePrefabType.TryCreateInstance() is not IGamePrefab gamePrefab)
            {
                throw new Exception($"Could not create instance of {gamePrefabType.Name}.");
            }
            
            gamePrefab.id = id;
            
            return gamePrefab;
        }
        
        public static GamePrefabWrapper CreateGamePrefabWrapper(string id, Type gamePrefabType)
        {
            var gamePrefab = CreateDefaultGamePrefab(id, gamePrefabType);
            
            return CreateGamePrefabWrapper(gamePrefab);
        }

        public static GamePrefabWrapper CreateGamePrefabWrapper(IGamePrefab gamePrefab)
        {
            if (GamePrefabGeneralSettingUtility.TryGetGamePrefabGeneralSetting(gamePrefab,
                    out var gamePrefabSetting) == false)
            {
                Debug.LogError($"Could not find GamePrefabGeneralSetting for {gamePrefab.GetType()}.");
                return null;
            }
            
            string path = gamePrefabSetting.gamePrefabDirectoryPath;

            if (path.EndsWith("/") == false)
            {
                path += "/";
            }
            
            path += gamePrefab.id.ToPascalCase();
            
            return CreateGamePrefabWrapper(path, gamePrefab);
        }
        
        public static GamePrefabWrapper CreateGamePrefabWrapper(string path, string id, Type gamePrefabType)
        {
            var gamePrefab = CreateDefaultGamePrefab(id, gamePrefabType);

            return CreateGamePrefabWrapper(path, gamePrefab);
        }

        public static GamePrefabWrapper CreateGamePrefabWrapper(string path, IGamePrefab gamePrefab)
        {
            gamePrefab.id.AssertIsNotNull(nameof(gamePrefab.id));

            if (gamePrefab.id.IsEmptyAfterTrim())
            {
                throw new ArgumentException($"{nameof(gamePrefab.id)} ID cannot be empty or whitespace.");
            }
            
            if (gamePrefab is ILocalizedStringOwnerConfig localizedStringOwner)
            {
                localizedStringOwner.AutoConfigureLocalizedString(default);
            }
            
            return CreateSingleGamePrefabWrapper(path, gamePrefab);
        }

        private static GamePrefabWrapper CreateSingleGamePrefabWrapper(string path, IGamePrefab gamePrefab)
        {
            if (path.EndsWith(".asset") == false)
            {
                path += ".asset";
            }

            AssetDatabase.Refresh();

            var absoluteFilePath = IOUtility.projectFolderPath.PathCombine(path);

            if (absoluteFilePath.ExistsFile())
            {
                Debug.LogWarning($"GamePrefabWrapper already exists at {path}.");
                return null;
            }

            var absoluteDirectoryPath = absoluteFilePath.GetDirectoryPath();
            absoluteDirectoryPath.CreateDirectory();
            
            var gamePrefabWrapper = GameCoreSetting.gamePrefabWrapperGeneralSetting
                .singleWrapperTemplate.CopyAssetTo(path);

            if (gamePrefabWrapper == null)
            {
                Debug.LogError(
                    $"Could not create GamePrefabWrapper which contains {gamePrefab} on Path : {path}");
                return null;
            }

            gamePrefabWrapper.gamePrefab = gamePrefab;

            if (GamePrefabGeneralSettingUtility.TryGetGamePrefabGeneralSetting(gamePrefab,
                    out var gamePrefabSetting))
            {
                gamePrefabSetting.AddDefaultGameTypeToGamePrefabWrapper(gamePrefabWrapper);
            }
            else
            {
                Debug.LogError($"Could not find GamePrefabGeneralSetting for {gamePrefab.GetType()}.");
            }

            gamePrefabWrapper.EnforceSave();
            OnGamePrefabWrapperCreated?.Invoke(gamePrefabWrapper);
            return gamePrefabWrapper;
        }
    }
}
#endif