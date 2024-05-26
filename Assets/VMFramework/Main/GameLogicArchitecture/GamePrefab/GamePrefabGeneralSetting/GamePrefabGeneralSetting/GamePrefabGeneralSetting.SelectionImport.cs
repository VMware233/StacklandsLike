#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        [Button("依选中目标添加")]
        [ShowIf(nameof(_ShowRegisterGameItemPrefabFromSelectionButton))]
        private void RegisterGameItemPrefabFromSelection()
        {
            foreach (var obj in Selection.objects.GetAllAssetsRecursively())
            {
                OnHandleRegisterGameItemPrefabFromSelectedObject(obj, false);
            }
            
            this.EnforceSave();
        }

        [Button("依选中目标添加（不重复）")]
        [ShowIf(nameof(_ShowRegisterGameItemPrefabFromSelectionButton))]
        private void RegisterUniqueGameItemPrefabFromSelection()
        {
            foreach (var obj in Selection.objects.GetAllAssetsRecursively())
            {
                OnHandleRegisterGameItemPrefabFromSelectedObject(obj, true);
            }
            
            this.EnforceSave();
        }

        [Button("依选中目标删除")]
        [ShowIf(nameof(_ShowRegisterGameItemPrefabFromSelectionButton))]
        private void UnregisterGameItemPrefabFromSelection()
        {
            foreach (var obj in Selection.objects.GetAllAssetsRecursively())
            {
                OnHandleUnregisterGameItemPrefabFromSelectedObject(obj);
            }
            
            this.EnforceSave();
        }

        protected virtual void OnHandleRegisterGameItemPrefabFromSelectedObject(
            Object obj, bool checkUnique)
        {

        }

        protected virtual void
            OnHandleUnregisterGameItemPrefabFromSelectedObject(
                Object obj)
        {

        }

        private bool _ShowRegisterGameItemPrefabFromSelectionButton()
        {
            return ShowRegisterGameItemPrefabFromSelectionButton(Selection
                .objects.GetAllAssetsRecursively());
        }

        protected virtual bool ShowRegisterGameItemPrefabFromSelectionButton(
            IEnumerable<Object> objects)
        {
            return false;
        }
    }
}
#endif