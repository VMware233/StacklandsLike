using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.UI
{
    public partial class QuestAndRecipeUIController
    {
        [ShowInInspector]
        private VisualElement recipeContainer;

        [ShowInInspector]
        private Dictionary<string, RecipeCategoryInfo> recipeCategories = new();
        
        [ShowInInspector]
        private Dictionary<ICardRecipe, RecipeEntryInfo> recipeEntries = new();

        private void OpenRecipePanel()
        {
            recipeContainer = rootVisualElement.Q(questAndRecipeUIPreset.recipeContainerName);

            foreach (var recipe in GamePrefabManager.GetGamePrefabsByGameType<ICardRecipe>(
                         questAndRecipeUIPreset.recipeGameTypeID))
            {
                AddRecipe(recipe);
            }
        }

        private void CloseRecipePanel()
        {
            recipeCategories.Clear();
            recipeEntries.Clear();
        }

        private RecipeCategoryInfo GetOrCreateRecipeCategory(string categoryID)
        {
            if (recipeCategories.TryGetValue(categoryID, out var categoryInfo))
            {
                return categoryInfo;
            }
            
            var foldoutText = GameType.GetGameTypeName(categoryID);
            var priority = 0;

            if (GameSetting.questAndRecipeUIGeneralSetting.recipeCategoryConfigs.TryGetConfigRuntime(
                    categoryID, out var categoryConfig))
            {
                foldoutText = categoryConfig.categoryName;
                priority = categoryConfig.priority;
            }

            var foldout = new Foldout()
            {
                text = foldoutText,
                value = true
            };

            categoryInfo = new RecipeCategoryInfo()
            {
                categoryID = categoryID,
                foldout = foldout,
                priority = priority
            };
            
            recipeCategories.Add(categoryID, categoryInfo);
            recipeContainer.Add(foldout);
            
            var sortedInfos = recipeCategories.Values.OrderBy(info => info.priority);

            foreach (var info in sortedInfos)
            {
                info.foldout.SetAsFirstSibling();
            }
            
            return categoryInfo;
        }

        private void AddRecipe(ICardRecipe recipe)
        {
            if (recipe == null)
            {
                Debug.LogWarning("Recipe is null.");
                return;
            }
            
            if (recipeEntries.ContainsKey(recipe))
            {
                Debug.LogWarning("Recipe already exists in the recipeEntries dictionary.");
                return;
            }
            
            var categoryInfo = GetOrCreateRecipeCategory(recipe.uniqueGameType.id);

            var foldout = new Foldout()
            {
                text = recipe.name,
                value = false
            };
            
            categoryInfo.foldout.Add(foldout);
            
            var consumptionLabels = new List<Label>();

            foreach (var config in recipe.consumptionConfigs)
            {
                var itemName = GamePrefabManager.GetGamePrefabNameWithWarning(config.itemID);
                
                var label = new Label
                {
                    text = config.count + "x " + itemName
                };
                
                consumptionLabels.Add(label);
                
                foldout.Add(label);
            }
            
            var descriptionLabel = new Label
            {
                text = recipe.description
            };
            
            foldout.Add(descriptionLabel);
            
            var recipeEntryInfo = new RecipeEntryInfo()
            {
                recipe = recipe,
                foldout = foldout,
                consumptionLabels = consumptionLabels,
                descriptionLabel = descriptionLabel
            };
            
            recipeEntries.Add(recipe, recipeEntryInfo);
        }
    }
}