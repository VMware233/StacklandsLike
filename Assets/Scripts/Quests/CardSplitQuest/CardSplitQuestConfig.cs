using System;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Quests
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class CardSplitQuestConfig : QuestConfig
    {
        public const string ID = "card_split_quest";

        public override Type gameItemType => typeof(CardSplitQuest);
    }
}