using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public class PersonCard : CreatureCard, IPersonCard
    {
        protected PersonCardConfig personCardConfig => (PersonCardConfig)gamePrefab;

        int INutritionRequiredCard.nutritionRequired => personCardConfig.nutritionRequired.GetValue() * count;

        [ShowInInspector]
        private Dictionary<string, IEquipmentCard> equippedCards = new();

        public void Equip(IEquipmentCard card)
        {
            var clone = card.GetClone();
            
            if (card.sourceContainer.TryRemoveItem(card) == false)
            {
                Debug.LogError(
                    $"Failed to remove card : {card} from source container : {card.sourceContainer}");
                return;
            }
            
            IGameItem.Destroy(card);

            var removedCards = new HashSet<IEquipmentCard>();

            foreach (var gameTypeID in clone.gameTypeSet.leafGameTypesID)
            {
                if (equippedCards.Remove(gameTypeID, out var removedCard))
                {
                    removedCards.Add(removedCard);
                }
                
                equippedCards[gameTypeID] = clone;
            }

            foreach (var removedCard in removedCards)
            {
                removedCard.OnUnequipFrom(this);
                CardGroupManager.CreateCardGroup(removedCard, group.GetPosition());
            }
            
            clone.OnEquipTo(this);
        }
    }
}