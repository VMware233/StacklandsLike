using System;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using VMFramework.OdinExtensions;

namespace StackLandsLike.Quests
{
    public class CardCollectionQuestConfig : QuestConfig
    {
        public override Type gameItemType => typeof(CardCollectionQuest);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [GamePrefabID(typeof(ICardConfig))]
        public string cardID;
    }
}