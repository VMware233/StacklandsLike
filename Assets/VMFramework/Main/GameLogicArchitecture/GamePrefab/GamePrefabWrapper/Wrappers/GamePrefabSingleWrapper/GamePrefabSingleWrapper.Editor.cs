#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabSingleWrapper
    {
        #region Align Name With ID

        private bool requiresNameAlignment
        {
            get
            {
                if (gamePrefab == null)
                {
                    return false;
                }

                return gamePrefab.id.ToPascalCase() != name;
            }
        }

        [Button, PropertyOrder(-100)]
        [ShowIf(nameof(requiresNameAlignment))]
        private void AlignNameWithID()
        {
            this.Rename(gamePrefab.id.ToPascalCase());
        }

        #endregion

        #region Change Type

        [Button, PropertyOrder(-100)]
        private void ChangeType()
        {
            if (GamePrefabGeneralSettingUtility.TryGetGamePrefabGeneralSetting(gamePrefab,
                    out var generalSetting) == false)
            {
                Debug.LogError("Could not find a GamePrefabGeneralSetting for the selected GamePrefab.");
                return;
            }

            var selector = new TypeSelector(generalSetting.baseGamePrefabType, false, false, targetType =>
            {
                gamePrefab = gamePrefab.ConvertToChildOrParent(targetType);
                
                this.EnforceSave();
            });

            selector.ShowInPopup();
        }

        #endregion
    }
}
#endif