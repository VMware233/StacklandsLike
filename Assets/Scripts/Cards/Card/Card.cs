using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;
using VMFramework.Property;

namespace StackLandsLike.Cards
{
    public partial class Card : ContainerItem, ICard
    {
        public override int maxStackCount => int.MaxValue;
        
        [ShowInInspector]
        public CardGroup group { get; private set; }

        public event Action<ICard, CardGroup> OnGroupChangedEvent; 

        protected override void OnCreate()
        {
            base.OnCreate();

            group = null;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            group = null;
        }

        public virtual bool CanStackWith(ICard card)
        {
            return true;
        }

        void ICard.SetGroup(CardGroup group)
        {
            this.group = group;
            
            OnGroupChangedEvent?.Invoke(this, group);
        }
    }
}