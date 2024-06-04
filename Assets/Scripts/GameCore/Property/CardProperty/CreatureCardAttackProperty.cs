using System;
using StackLandsLike.Cards;
using VMFramework.GameLogicArchitecture;
using VMFramework.Property;

namespace StackLandsLike.GameCore
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class CreatureCardAttackProperty : PropertyConfig
    {
        public const string ID = "creature_card_attack_property";

        public override Type targetType => typeof(ICreatureCard);

        public override string GetValueString(object target)
        {
            var creature = (ICreatureCard)target;
            
            return creature.attack.ToString();
        }
    }
}