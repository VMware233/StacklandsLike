using System.Collections.Generic;
using StackLandsLike.Cards;
using UnityEngine.UIElements;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public class RecipeEntryInfo
    {
        public ICardRecipe recipe;
        
        public Foldout foldout;
        
        public List<IconLabelVisualElement> consumptionLabels;
        
        public Label descriptionLabel;
    }
}