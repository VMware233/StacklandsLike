using System.Runtime.CompilerServices;
using UnityEngine;

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

            if (group.CanAddCard(card) == false)
            {
                return;
            }
            
            var oldGroup = card.group;
            if (oldGroup != null)
            {
                oldGroup.RemoveCard(card);
                if (oldGroup.count == 0)
                {
                    CardGroupManager.DestroyCardGroup(oldGroup);
                }
            }
            
            group.AddCard(card);
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
            
            group.RemoveCard(card);

            var newGroup = CardGroupManager.CreateCardGroup(card, position);
            
            return newGroup;
        }
    }
}