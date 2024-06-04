using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Property;

namespace StackLandsLike.Cards
{
    public class CreatureCard : Card, ICreatureCard, IContainerItem
    {
        protected CreatureCardConfig creatureCardConfig => (CreatureCardConfig)gamePrefab;

        public override int maxStackCount => 1;

        [ShowInInspector]
        public BaseIntProperty health;
        
        [ShowInInspector]
        public BaseBoostIntProperty maxHealth;
        
        [ShowInInspector]
        public BaseIntProperty attack;
        
        [ShowInInspector]
        public BaseIntProperty defense;

        private CardGroup lastGroup;

        protected override void OnCreate()
        {
            base.OnCreate();

            maxHealth = new(creatureCardConfig.defaultMaxHealth.GetValue());
            health = new(maxHealth);
            attack = new(creatureCardConfig.defaultAttack.GetValue());
            defense = new(creatureCardConfig.defaultDefense.GetValue());
        }

        public void OnRemoveFromContainer(IContainer container)
        {
            lastGroup = group;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var generationConfig in creatureCardConfig.dropCardConfigs.GetValue())
            {
                var card = generationConfig.GenerateItem();
                CardGroupManager.CreateCardGroup(card, lastGroup.GetPosition());
            }
        }

        #region Interface Implementation
        
        int ICreatureCard.health => health;

        int ICreatureCard.maxHealth => maxHealth;

        int ICreatureCard.attack
        {
            get => attack;
            set => attack.value = value;
        }

        int ICreatureCard.defense
        {
            get => defense;
            set => defense.value = value;
        }

        #endregion
    }
}