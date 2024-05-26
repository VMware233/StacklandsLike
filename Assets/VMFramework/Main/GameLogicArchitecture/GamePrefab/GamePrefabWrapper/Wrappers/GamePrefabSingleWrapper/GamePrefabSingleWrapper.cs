using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameLogicArchitecture
{
    [CreateAssetMenu(fileName = "New GamePrefabSingleWrapper", 
        menuName = "VMFramework/GamePrefabSingleWrapper")]
    public partial class GamePrefabSingleWrapper : GamePrefabWrapper, INameOwner
    {
        [HideLabel]
        public IGamePrefab gamePrefab;
        
        public override IEnumerable<IGamePrefab> GetGamePrefabs()
        {
            yield return gamePrefab;
        }

        #region Interface Implementation

        string INameOwner.name
        {
            get
            {
                if (gamePrefab == null)
                {
                    if (this != null)
                    {
                        return name;
                    }

                    return "Null GamePrefabSingleWrapper";
                }

                return gamePrefab.name;
            }
        }

        #endregion
    }
}