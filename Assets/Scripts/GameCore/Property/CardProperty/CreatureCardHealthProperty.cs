using System;
using StackLandsLike.Cards;
using VMFramework.GameLogicArchitecture;
using VMFramework.Property;

namespace StackLandsLike.GameCore
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class CreatureCardHealthProperty : PropertyConfig
    {
        public const string ID = "creature_card_health_property";
        
        public override Type targetType => typeof(ICreatureCard);
        
        public override string GetValueString(object target)
        {
            var creatureCard = (ICreatureCard)target;
            return $"{creatureCard.health}/{creatureCard.maxHealth}";
        }
    }
}