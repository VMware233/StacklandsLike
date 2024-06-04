using System;
using StackLandsLike.Cards;
using VMFramework.GameLogicArchitecture;
using VMFramework.Property;

namespace StackLandsLike.GameCore
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class CreatureCardDefenseProperty : PropertyConfig
    {
        public const string ID = "creature_card_defense_property";

        public override Type targetType => typeof(ICreatureCard);

        public override string GetValueString(object target)
        {
            var creature = (ICreatureCard)target;
            
            return creature.defense.ToString();
        }
    }
}