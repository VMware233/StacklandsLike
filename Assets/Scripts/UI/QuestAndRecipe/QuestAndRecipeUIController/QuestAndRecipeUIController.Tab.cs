using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public partial class QuestAndRecipeUIController
    {
        private VisualElement questTab;
        private VisualElement recipeTab;

        private VisualElement questTabUnselectedIcon;
        private VisualElement recipeTabUnselectedIcon;
        
        private void OpenTab()
        {
            questTab = rootVisualElement.Q(questAndRecipeUIPreset.questTabName);
            recipeTab = rootVisualElement.Q(questAndRecipeUIPreset.recipeTabName);

            questTabUnselectedIcon = rootVisualElement.Q(questAndRecipeUIPreset.questTabUnselectedIconName);
            recipeTabUnselectedIcon = rootVisualElement.Q(questAndRecipeUIPreset.recipeTabUnselectedIconName);
            
            QuestTabMouseDown(null);
            questTab.RegisterCallback<MouseDownEvent>(QuestTabMouseDown);
            recipeTab.RegisterCallback<MouseDownEvent>(RecipeTabMouseDown);
        }

        private void QuestTabMouseDown(MouseDownEvent e)
        {
            questTabUnselectedIcon.style.display = DisplayStyle.None;
            recipeTabUnselectedIcon.style.display = DisplayStyle.Flex;
            
            questContainer.style.display = DisplayStyle.Flex;
            recipeContainer.style.display = DisplayStyle.None;
        }
        
        private void RecipeTabMouseDown(MouseDownEvent e)
        {
            recipeTabUnselectedIcon.style.display = DisplayStyle.None;
            questTabUnselectedIcon.style.display = DisplayStyle.Flex;
            
            recipeContainer.style.display = DisplayStyle.Flex;
            questContainer.style.display = DisplayStyle.None;
        }

        private void CloseTab()
        {
            
        }
        
        
    }
}