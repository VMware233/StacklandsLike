using System;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using VMFramework.OdinExtensions;

namespace StackLandsLike.Quests
{
    public class CardCraftQuestConfig : QuestConfig
    {
        public override Type gameItemType => typeof(CardCraftQuest);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [GamePrefabID(typeof(ICardRecipe))]
        public string recipeID;
    }
}