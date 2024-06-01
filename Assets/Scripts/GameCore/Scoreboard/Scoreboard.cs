using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class Scoreboard : ManagerBehaviour<Scoreboard>, IManagerBehaviour
    {
        [ShowInInspector]
        private static readonly HashSet<string> treeChoppingRecipesID = new();
        
        [ShowInInspector]
        public static int treeChoppingCount { get; private set; }
        
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            CardCraftManager.OnRecipeCompleted += OnRecipeCompleted;
        }

        void IInitializer.OnInitComplete(Action onDone)
        {
            foreach (var treeID in GameSetting.scoreboardGeneralSetting.treesID)
            {
                if (GamePrefabManager.TryGetActiveGamePrefab(treeID,
                        out IProducerCardConfig treeConfig) == false)
                {
                    Debug.LogError($"Could not find tree with ID {treeID} or it is not active or not a producer card.");
                    continue;
                }
                
                treeChoppingRecipesID.Add(treeConfig.lastProductionRecipeID);
            }
            
            onDone();
        }

        private static void OnRecipeCompleted(CardGroup group, ICardRecipe recipe)
        {
            if (treeChoppingRecipesID.Contains(recipe.id))
            {
                treeChoppingCount++;
            }
        }

        public static void ResetScoreboard()
        {
            treeChoppingCount = 0;
        }
    }
}