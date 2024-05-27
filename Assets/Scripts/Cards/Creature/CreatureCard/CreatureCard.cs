using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Property;

namespace StackLandsLike.Cards
{
    public class CreatureCard : Card, ICreatureCard
    {
        protected CreatureCardConfig creatureCardConfig => (CreatureCardConfig)gamePrefab;

        [ShowInInspector]
        public BaseIntProperty health;
        
        [ShowInInspector]
        public BaseBoostIntProperty maxHealth;
        
        [ShowInInspector]
        public BaseBoostIntProperty attack;

        protected override void OnCreate()
        {
            base.OnCreate();

            maxHealth = new(creatureCardConfig.defaultMaxHealth.GetValue());
            health = new(maxHealth);
            attack = new(creatureCardConfig.defaultAttack.GetValue());
        }
    }
}