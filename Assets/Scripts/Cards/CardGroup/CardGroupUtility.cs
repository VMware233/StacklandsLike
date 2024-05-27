using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Containers;

namespace StackLandsLike.Cards
{
    public static class CardGroupUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveToGroup(this ICard card, CardGroup group)
        {
            if (group == null)
            {
                Debug.LogError("CardGroup is null");
                return;
            }

            if (group.cardContainer.CanAddCard(card) == false)
            {
                return;
            }
            
            var oldGroup = card.group;
            if (oldGroup != null)
            {
                if (oldGroup.cardContainer.TryPopItemTo(card, group.cardContainer) == false)
                {
                    Debug.LogError("Failed to move card from old group to new group");
                    return;
                }
                
                if (oldGroup.cardContainer.totalItemCount == 0)
                {
                    CardGroupManager.DestroyCardGroup(oldGroup);
                }
            }
            else
            {
                group.cardContainer.TryAddItem(card);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CardGroup MoveOutOfGroup(this ICard card, Vector2 position)
        {
            var group = card.group;
            
            if (group == null)
            {
                return null;
            }

            if (group.count == 1)
            {
                return null;
            }
            
            if (group.cardContainer.TryRemoveItem(card) == false)
            {
                Debug.LogError($"Failed to remove {card} from group {group.name}");
                return null;
            }

            var newGroup = CardGroupManager.CreateCardGroup(card, position);
            
            return newGroup;
        }
    }
}