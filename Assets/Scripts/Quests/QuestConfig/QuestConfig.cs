using System;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Quests
{
    public class QuestConfig : DescribedGamePrefab, IQuestConfig
    {
        public override Type gameItemType => typeof(Quest);

        protected override string idSuffix => "quest";
    }
}