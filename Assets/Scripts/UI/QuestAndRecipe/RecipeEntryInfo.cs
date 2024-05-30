using System.Collections.Generic;
using StackLandsLike.Cards;
using UnityEngine.UIElements;

namespace StackLandsLike.UI
{
    public class RecipeEntryInfo
    {
        public ICardRecipe recipe;
        
        public Foldout foldout;
        
        public List<Label> consumptionLabels;
        
        public Label descriptionLabel;
    }
}