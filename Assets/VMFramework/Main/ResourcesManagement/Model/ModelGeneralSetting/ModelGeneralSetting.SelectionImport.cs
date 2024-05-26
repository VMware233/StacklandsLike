#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class ModelGeneralSetting
    {
        protected override void OnHandleRegisterGameItemPrefabFromSelectedObject(Object obj, bool checkUnique)
        {
            if (obj is not GameObject gameObject)
            {
                return;
            }

            if (gameObject.IsPrefabAsset() == false)
            {
                return;
            }

            if (checkUnique)
            {
                if (GamePrefabManager.GetAllGamePrefabs<ModelPreset>()
                    .Any(prefab => prefab.readyMadeModelPrefab == gameObject))
                {
                    Debug.LogWarning($"为了放置重复，忽略了{gameObject.name}的添加");
                    return;
                }
            }

            GamePrefabWrapperCreator.CreateGamePrefabWrapper(new ModelPreset()
            {
                id = gameObject.name.ToSnakeCase(),
                readyMadeModelPrefab = gameObject
            });
        }

        protected override void OnHandleUnregisterGameItemPrefabFromSelectedObject(Object obj)
        {
            if (obj is not GameObject gameObject)
            {
                return;
            }

            if (gameObject.IsPrefabAsset() == false)
            {
                return;
            }

            GamePrefabWrapperRemover.RemoveGamePrefabWrapperWhere<ModelPreset>(room =>
                room.readyMadeModelPrefab == gameObject);
        }

        protected override bool ShowRegisterGameItemPrefabFromSelectionButton(IEnumerable<Object> objects)
        {
            return objects.Any(obj => obj is GameObject go && go.IsPrefabAsset());
        }
    }
}
#endif