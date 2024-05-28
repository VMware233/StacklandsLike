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
        
        [ShowInInspector]
        public BaseIntProperty defense;

        protected override void OnCreate()
        {
            base.OnCreate();

            maxHealth = new(creatureCardConfig.defaultMaxHealth.GetValue());
            health = new(maxHealth);
            attack = new(creatureCardConfig.defaultAttack.GetValue());
            defense = new(creatureCardConfig.defaultDefense.GetValue());
        }

        #region Interface Implementation
        
        int ICreatureCard.health => health;

        int ICreatureCard.maxHealth => maxHealth;

        int ICreatureCard.attack => attack;

        int ICreatureCard.defense => defense;

        #endregion
    }
}