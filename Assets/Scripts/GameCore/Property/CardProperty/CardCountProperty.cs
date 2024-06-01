using System;
using StackLandsLike.Cards;
using VMFramework.GameLogicArchitecture;
using VMFramework.Property;

namespace StackLandsLike.GameCore
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class CardCountProperty : PropertyConfig
    {
        public const string ID = "card_count_property";

        public override Type targetType => typeof(ICard);

        public override string GetValueString(object target)
        {
            var card = (ICard) target;
            return card.count.ToString();
        }
    }
}